using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.Models;

namespace Uloq.SDK.Connectors
{
    internal class HttpConnector
    {
        private readonly ConnectionModel _connectionModel;

        public HttpConnector(ConnectionModel model)
        {
            this._connectionModel = model;
        }

        public async Task<OutcomeObject<T>> PostAsync<T>(string endpoint, string jsonPayload)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                //Send the HMAC key              
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("amx", GenerateHMAC(jsonPayload));                

                var response = await httpClient.PostAsync($"{_connectionModel.ApiUrl}/{endpoint}",
                    new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<OutcomeObject<T>>(content);
                }
                return new OutcomeObject<T>() { Success = false, Message = response.ReasonPhrase };
            }
            catch (Exception ex)
            {
                return new OutcomeObject<T>() { Success = false, Message = ex.Message };
            }
        }

        public async Task<OutcomeObject> PostAsync(string endpoint, string jsonPayload)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                //Send the HMAC key              
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("amx", GenerateHMAC(jsonPayload));

                var response = await httpClient.PostAsync($"{_connectionModel.ApiUrl}/{endpoint}",
                    new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<OutcomeObject>(content);
                }
                return new OutcomeObject() { Success = false, Message = response.ReasonPhrase };
            }
            catch (Exception ex)
            {
                return new OutcomeObject() { Success = false, Message = ex.Message };
            }
        }

        private string GenerateHMAC(string jsonPayload)
        {
            string appId = _connectionModel.ApiKey ?? "";
            string apiKey = _connectionModel.ApiSecret ?? "";

            string nonce = Guid.NewGuid().ToByteArray().ToSha256Base64String();

            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan currentTs = DateTime.UtcNow - epochStart;
            var requestTimeStamp = Convert.ToUInt64(currentTs.TotalSeconds).ToString();

            var requestContentBase64String = jsonPayload.ToSha256Base64String();
            string data = String.Format("{0}{1}{2}{3}", appId, requestTimeStamp, nonce, requestContentBase64String);

            var secretKeyBytes = Convert.FromBase64String(apiKey);
            byte[] dataByteArray = data.GetBytes();
            using (HMACSHA256 hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] signatureBytes = hmac.ComputeHash(dataByteArray);
                var convertB64 = Convert.ToBase64String(signatureBytes);

                return $"{appId}:{convertB64}:{nonce}:{requestTimeStamp}";
            }
        }
    }
}

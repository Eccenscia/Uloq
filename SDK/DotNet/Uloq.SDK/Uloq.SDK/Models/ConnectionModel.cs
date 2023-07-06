using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Uloq.SDK.Models
{
    public class ConnectionModel
    {
        public bool UseSandbox { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiUrl { 
            get {
                if (UseSandbox)                
                    return "https://sandbox-api.uloq.io";
                else
                    return "https://api.uloq.io";
            } 
        }

        private string GenerateHMAC(string jsonPayload)
        {
            string appId = ApiKey ?? "";
            string apiKey = ApiSecret ?? "";

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

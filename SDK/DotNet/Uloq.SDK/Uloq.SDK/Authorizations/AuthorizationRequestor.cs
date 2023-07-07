using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uloq.SDK.Connectors;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.Models;

namespace Uloq.SDK.Authorizations
{
    public class AuthorizationRequestor
    {
        private readonly ConnectionModel _connectionModel;

        public AuthorizationRequestor(ConnectionModel connectionModel)
        {
            _connectionModel = connectionModel;
        }

        /// <summary>
        /// Create a push notification authorization request
        /// </summary>
        /// <param name="keyIdentifier"></param>
        /// <param name="notificationIdentifier"></param>
        /// <param name="expiryDate"></param>
        /// <param name="category"></param>
        /// <param name="actionTitle"></param>
        /// <param name="actionMessage"></param>
        /// <param name="metadata"></param>
        /// <returns>boolean: successfully sent</returns>
        public async Task<bool> CreateAuthorization(string keyIdentifier, string notificationIdentifier, DateTime expiryDate, string category, string actionTitle, string actionMessage, string metadata)
        {
            var authorizationRequest = new AuthorizationRequest()
            {
                KeyIdentifier = keyIdentifier,
                NotificationIdentifier = notificationIdentifier,
                ActionMessage = actionMessage,
                ActionTitle = actionTitle,
                Category = category,
                ExpiryDateUTC = expiryDate.ToUniversalTime().ToString(),
                Metadata = metadata
            };
            return await CreateAuthorization(authorizationRequest);
        }


        /// <summary>
        /// Create a push notification authorization request
        /// </summary>
        /// <param name="authorizationRequest"></param>
        /// <returns>boolean: the success of pushing the request to the network</returns>
        public async Task<bool> CreateAuthorization(AuthorizationRequest authorizationRequest)
        {
            HttpConnector httpConnector = new HttpConnector(_connectionModel);
            OutcomeObject outcomeObject = await httpConnector.PostAsync("requestauthorization", authorizationRequest.ToJson());
            return outcomeObject.Success;
        }

        /// <summary>
        /// Request the notification response from the Eccenscia services
        /// </summary>
        /// <param name="notificationDetailsRequest"></param>
        /// <returns>
        /// Notification Detail Request which includes
        /// <param name="keyIdentifier">Base64 string.</param>
        /// <param name="notificationIdentifier">notificationIdentifier.</param>
        /// <param name="status">status.</param>
        /// <param name="payload">payload.</param>
        /// <param name="signature">Base64 representation of the transaction signature. Order - KeyIdentifier, NotificationIdentifier, Status, Payload (assending order).</param>
        /// <param name="publicKey">publicKey.</param>
        /// <param name="identifierMetadata">Base64 representation of the encrypted metadata associated with the identifier.</param></returns>
        public async Task<NotificationDetailsResponse> GetAuthorizationResponse(NotificationDetailsRequest notificationDetailsRequest)
        {
            HttpConnector httpConnector = new HttpConnector(_connectionModel);
            OutcomeObject<NotificationDetailsResponse> outcomeObject = await httpConnector.PostAsync<NotificationDetailsResponse>("gettransactionresult", notificationDetailsRequest.ToJson());
            if (outcomeObject.Result != null && outcomeObject.Success)
                return outcomeObject.Result;

            return null;
        }

        /// <summary>
        /// Periodically runs the GetAuthorizationResponse function and times out after a defined period.
        /// </summary>
        /// <param name="notificationDetailsRequest">The notification details request.</param>
        /// <param name="interval">The interval between each request.</param>
        /// <param name="timeout">The timeout period.</param>
        /// <returns>The notification details response if successful within the timeout period; otherwise, null.</returns>
        public async Task<NotificationDetailsResponse> RunAuthorizationResponseTask(NotificationDetailsRequest notificationDetailsRequest, TimeSpan interval, TimeSpan timeout)
        {
            DateTime endTime = DateTime.Now + timeout;

            while (DateTime.Now < endTime)
            {
                NotificationDetailsResponse response = await GetAuthorizationResponse(notificationDetailsRequest);
                if (response != null)
                    return response;

                await Task.Delay(interval);
            }

            return null; // Timed out
        }

    }
}

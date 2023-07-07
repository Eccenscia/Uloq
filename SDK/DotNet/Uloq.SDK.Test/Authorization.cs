using Uloq.SDK.Authorizations;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.Models;

namespace Uloq.SDK.Test
{
    public class Authorization
    {
        private string _keyIdentifier = "<insert your uloq key identifier>";
        private string _notificationIdentifier = Guid.NewGuid().ToString();
        

        [Fact(DisplayName = "Create Authorization with Model")]
        public async void CreateAuthorizationRequest()
        {
            var request = new AuthorizationRequest
            {
                ActionMessage = "Test Message",
                ActionTitle = "Test Title",
                Category = "Test Category",
                ExpiryDateUTC = DateTime.UtcNow.ToString(),
                KeyIdentifier = _keyIdentifier,
                Metadata = "Test Metadata",
                NotificationIdentifier = _notificationIdentifier
            };

            AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
            Assert.True(await authorizationRequestor.CreateAuthorization(request), "Authorization requested");
        }

        [Fact(DisplayName = "Create Authorization with fields")]
        public async void CreateAuthorizationRequestWithFields()
        {
            var request = new AuthorizationRequest
            {
                ActionMessage = "Test Message",
                ActionTitle = "Test Title",
                Category = "Test Category",
                ExpiryDateUTC = DateTime.UtcNow.ToString(),
                KeyIdentifier = _keyIdentifier,
                Metadata = "Test Metadata",
                NotificationIdentifier = _notificationIdentifier
            };

            AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
            Assert.True(
                await authorizationRequestor.CreateAuthorization(
                    request.KeyIdentifier, 
                    request.NotificationIdentifier,
                    DateTime.UtcNow.AddMinutes(1), 
                    request.Category, 
                    request.ActionTitle, 
                    request.ActionMessage, 
                    request.Metadata), 
                "Authorization requested");
        }

        [Fact(DisplayName = "Get an authorization response")]
        public async void GetRequestResponse()
        {
            AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));
            var response = await authorizationRequestor.GetAuthorizationResponse(new NotificationDetailsRequest(_notificationIdentifier));

            int counter = 0;
            while (response == null && counter < 30)
            {                
                Thread.Sleep(1000);
                counter++;
                response = await authorizationRequestor.GetAuthorizationResponse(new NotificationDetailsRequest(_notificationIdentifier));
            }

            if (response != null)
            {
                Assert.True(response.Status == NotificationDetailsResponse.StatusEnum.Approved || response.Status == NotificationDetailsResponse.StatusEnum.Declined, "Status is pending");
                Assert.True(response.Signature != null, "Signature is not null");
                Assert.True(response.KeyIdentifier == _keyIdentifier, "Key identifier is correct");
            }
        }

        [Fact(DisplayName = "Test response timeout")]
        public async Task RunAuthorizationResponseTask_ShouldReturnResponseWithinTimeoutPeriod()
        {
            // Arrange
            AuthorizationRequestor authorizationRequestor = new AuthorizationRequestor(Models.ConnectionModel.CreateConnection("test", "test", true));

            var notificationDetailsRequest = new NotificationDetailsRequest();
            var interval = TimeSpan.FromSeconds(1);
            var timeout = TimeSpan.FromSeconds(10);

            // Act
            var task = authorizationRequestor.RunAuthorizationResponseTask(notificationDetailsRequest, interval, timeout);
            var response = await task;

            if (response != null)            
                Assert.NotNull(response);
        }
    }
}
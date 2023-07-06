using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.QR;

namespace Uloq.SDK.Test
{
    public class QR
    {
        [Fact(DisplayName = "Generate Signing QR Code")]
        public async void GenerateSigningQR()
        {
            var connection = Models.ConnectionModel.CreateConnection("test", "test", true);
            QRGenerator qrGenerator = new QRGenerator(connection);
            var output = await qrGenerator.GenerateQRCode(new QRCodeRequest() { Category = "Test", ActionTitle = "Sign Test", ActionMessage = "Test Message", Metadata = "Test Metadata", PublicKey = "", RequestType = QRCodeRequest.RequestTypeEnum.Sign });
            Assert.True(output != null, "No output received");

            if (output != null)
            {
                Assert.True(!String.IsNullOrEmpty(output.Image), "Image is not empty");
                Assert.True(!String.IsNullOrEmpty(output.Url), "URL is not empty");
                Assert.True(!String.IsNullOrEmpty(output.NotificationIdentifier), "Notification Identifier is not empty");
            }
        }

        [Fact(DisplayName = "Generate Key Exchange QR Code")]
        public async void GenerateKeyExchangeingQR()
        {
            var connection = Models.ConnectionModel.CreateConnection("test", "test", true);
            QRGenerator qrGenerator = new QRGenerator(connection);
            var output = await qrGenerator.GenerateQRCode(new QRCodeRequest() { Category = "Test", ActionTitle = "Key Exchange Test", ActionMessage = "Test Message", Metadata = "Test Metadata", PublicKey = "", RequestType = QRCodeRequest.RequestTypeEnum.KeyExchange });
            Assert.True(output != null, "No output received");

            if (output != null)
            {
                Assert.True(!String.IsNullOrEmpty(output.Image), "Image is not empty");
                Assert.True(!String.IsNullOrEmpty(output.Url), "URL is not empty");
                Assert.True(!String.IsNullOrEmpty(output.NotificationIdentifier), "Notification Identifier is not empty");
            }
        }
    }
}
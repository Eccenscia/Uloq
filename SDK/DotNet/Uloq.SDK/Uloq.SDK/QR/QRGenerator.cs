using System.Threading.Tasks;
using Uloq.SDK.Connectors;
using Uloq.SDK.Eccenscia.Services.Models.UloqRequestor;
using Uloq.SDK.Models;

namespace Uloq.SDK.QR
{
    public class QRGenerator
    {
        private readonly ConnectionModel _connectionModel;

        public QRGenerator(ConnectionModel connectionModel)
        {
            _connectionModel = connectionModel;
        }

        /// <summary>
        /// Generate the QR Code from the Uloq services
        /// </summary>
        /// <param name="jsonPayload"></param>
        /// <returns>QRcodeResponse</returns>
        public async Task<QRCodeResponse> GenerateQRCode(QRCodeRequest request)
        {
            HttpConnector httpConnector = new HttpConnector(_connectionModel);
            OutcomeObject<QRCodeResponse> result = await httpConnector.PostAsync<QRCodeResponse>("createqrcode", request.ToJson());

            if (result.Success)            
                return result.Result;            
            
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Uloq.SDK.Models
{
    public class ConnectionModel
    {
        private bool _useSandBox = false;
        public bool UseSandbox
        {
            get => _useSandBox;
            set
            {
                _useSandBox = value;
                if (value)
                    ApiUrl = "https://api-sandbox.uloq.com/uloq/requestor";
                else
                    ApiUrl = "https://api.uloq.com/uloq/requestor";
            }
        }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiUrl { get; set; }
        public static ConnectionModel CreateConnection(string apiKey, string apiSecret, bool useSandbox = false)
        {
            return new ConnectionModel()
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret,
                UseSandbox = useSandbox
            };
        }
    }
}

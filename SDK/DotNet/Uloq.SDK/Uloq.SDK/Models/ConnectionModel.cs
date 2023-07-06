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
                    return "https://api-sandbox.uloq.io/uloq/requestor";
                else
                    return "https://api.uloq.io/uloq/requestor";
            } 
        } 
        
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

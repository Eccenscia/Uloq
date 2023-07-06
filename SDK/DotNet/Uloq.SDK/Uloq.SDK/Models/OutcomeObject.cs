using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Uloq.SDK.Models
{
    public class OutcomeObject<T>
    {
        public int StatusCode { get; set; } = 200;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public T Result { get; set; } = default;
    }

    public class OutcomeObject
    {
        public int StatusCode { get; set; } = 200;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public object Result { get; set; } = null;
    }
}

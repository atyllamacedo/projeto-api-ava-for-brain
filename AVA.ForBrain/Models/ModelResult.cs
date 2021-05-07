using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AVA.ForBrain.Models
{
    public class ModelResult
    {
        public Object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public Boolean Valid { get; set; }
    }
}

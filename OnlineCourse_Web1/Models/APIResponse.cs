using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse_Web1.Models
{
    public class APIResponse
    {
        public APIResponse()
        { 
           ErrorMessage = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public List<string> ErrorMessage { get; set; }

        public object Result { get; set; }
    }
}

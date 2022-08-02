using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace PartnerWebApi.Models.OutgoingModels
{
    public class BaseResponse: HttpResponseMessage
    {
        public string Message { get; set; }
        public bool IsError { get; set; }

        public BaseResponse(HttpStatusCode statusCode, string message, bool isError = false) : base(statusCode)
        {
            IsError = isError;
            Message = message;
        }

        public BaseResponse(): this(HttpStatusCode.OK, "")
        {
            
        }
    }
}
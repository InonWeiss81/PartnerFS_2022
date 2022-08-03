using System.Net;

namespace PartnerWebApi.Models.OutgoingModels
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public bool IsError { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public BaseResponse(HttpStatusCode statusCode, string message, bool isError = false)
        {
            StatusCode = statusCode;
            IsError = isError;
            Message = message;
        }

        public BaseResponse(): this(HttpStatusCode.OK, "")
        {
            
        }
    }
}
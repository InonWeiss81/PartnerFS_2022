using static PartnerWebApi.Models.Enums.ResponseMessages;
using System.Net;

namespace PartnerWebApi.Models.OutgoingModels
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public bool IsError { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        #region cTor
        public BaseResponse(HttpStatusCode statusCode, Messages message, bool isError = false)
        {
            StatusCode = statusCode;
            IsError = isError;
            Message = TranslateMessage(message);
        }

        public BaseResponse(HttpStatusCode statusCode, string message, bool isError = false)
        {
            StatusCode = statusCode;
            IsError = isError;
            Message = message;
        }
        #endregion
    }
}
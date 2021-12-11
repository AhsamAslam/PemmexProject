using PemmexCommonLibs.Domain.Enums;

namespace PemmexCommonLibs.Application.Helpers
{
    public class ResponseMessage
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public EResponse Code { get; set; }
        public dynamic ResponseData { get; set; }

        public ResponseMessage(bool status,EResponse code, string message,dynamic data)
        {
            IsSuccess = status;
            Message = message;
            ResponseData = data;
            Code = code;
        }
    }
}

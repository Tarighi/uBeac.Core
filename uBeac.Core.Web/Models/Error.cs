using System;

namespace uBeac.Core.Web
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Trace { get; set; }

        public Error(Exception exception)
        {
            Code = "Unhandled-500";
            Message = exception.Message;
            Trace = exception.StackTrace;
        }
    }
}

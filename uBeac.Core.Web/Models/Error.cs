using System;

namespace uBeac.Core.Web
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public object Trace { get; set; }

        public Error()
        {

        }

        public Error(Exception exception)
        {
            Code = "Unhandled-500";
            Description = exception.Message;
            Trace = exception.StackTrace;
        }
    }
}

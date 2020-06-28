using System;

namespace SantexLeague.Common.Exceptions
{
    public class ExceptionUtil
    {
        public static string Message(Exception exception)
        {
            if (exception.InnerException != null) {
                return Message(exception.InnerException);
            }
            return exception.Message;
        }
    }
}
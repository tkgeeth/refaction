using System;

namespace refactor_me.Logger
{
    public static class Logging
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string message, Exception exp)
        {
            log.Info(message, exp);
        }

        public static void Info(string message)
        {
            log.Info(message);
        }

        public static void Error(string message, Exception exp)
        {
            log.Error(message, exp);
        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Debug(string message, Exception exp)
        {
            log.Debug(message, exp);
        }

        public static void Debug(string message)
        {
            log.Debug(message);
        }
    }
}
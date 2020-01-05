using System;

namespace Domain.Exception
{
    public static class MessageUtility
    {
        public static void ShowWarningMessage(Object owner, string message)
        {
            //Abort();
            Console.WriteLine(String.Concat(owner, ": ", message, "-","Предупреждение"));
        }

        public static void ShowErrorMessage(Object owner, string message)
        {
            //Abort();
            Console.WriteLine(String.Concat(owner, ": ", message, "-", "Ошибка"));
        }

        public static void ShowUnhandledError(Object owner, System.Exception ex)
        {
            //Abort();
            Console.WriteLine(String.Concat(owner, ": ", ex.Message, "-", "Необработанная ошибка"));
        }

        public static void ShowInformationMessage(Object owner, string message)
        {
            //Abort();
            Console.WriteLine(String.Concat(owner, ": ", message, "-", "Информация"));
        }

        public static void ShowValidationErrorSummary(Object owner)
        {
            //Abort();
            Console.WriteLine(String.Concat(owner, ": ", "Пожалуйста, исправьте ошибки и повторите сохранение."));
        }

        public static void ShowValidationMessage(Object owner, string message)
        {
            //Abort();
            Console.WriteLine(String.Concat(owner, ": ", message, "-", "Ошибка проверки данных"));
        }
    }
}

using System;

namespace BLL.Exception
{
    public static class MessageUtility
    {
        public static void ShowWarningMessage(string message)
        {
            Console.WriteLine(String.Concat(" ", message, "-","Предупреждение"));
        }

        public static void ShowErrorMessage(string message)
        {
            Console.WriteLine(String.Concat(" ", message, "-", "Ошибка"));
        }

        public static void ShowUnhandledError(System.Exception ex)
        {
            Console.WriteLine(String.Concat(" ", ex.Message, "-", "Необработанная ошибка"));
        }

        public static void ShowInformationMessage(string message)
        {
            Console.WriteLine(String.Concat(" ", message, "-", "Информация"));
        }

        public static void ShowValidationErrorSummary()
        {
            Console.WriteLine(String.Concat(" ", "Пожалуйста, исправьте ошибки и повторите сохранение."));
        }

        public static void ShowValidationMessage(string message)
        {
            Console.WriteLine(String.Concat(" ", message, "-", "Ошибка проверки данных"));
        }
    }
}

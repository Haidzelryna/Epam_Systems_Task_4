using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;


namespace Domain.Exception
{
    public static class ExceptionUtility
    {
        private static readonly Dictionary<int, string> SqlExceptionUserMessagesMap = new Dictionary<int, string>()
        {
            //{ 0, Resources.ErrorAccessDenied },
            { 1, "Записи в базе данных заблокированы другим пользователем.\nПовторите попытку внесения изменений позже." },
            { 2, "Ошибка при обновлении. Проверьте данные на дублирование." }
        };

        public static void ProcessSqlException(Object owner, SqlException ex)
        {
            string message;

            //Abort();
            if (ex.Number >= 50000)
            {
                message = GetSqlExceptionErrorMessage(ex);
                MessageUtility.ShowErrorMessage(owner, message);
            }
            else if (SqlExceptionUserMessagesMap.TryGetValue(ex.Number, out message))
            {
#if DEBUG
                message = string.Format("{0}\n{1}", message, ex.Message);
#endif
                MessageUtility.ShowErrorMessage(owner, message);
            }
            else
            {
                //DisplayException(owner, ex);
            }
        }

        public static string GetSqlExceptionErrorMessage(SqlException ex)
        {
            return ex.Errors
                .Cast<SqlError>()
                .Where(x => x.Class > 0) // Игнорируем сообщения от оператора PRINT
                .Select(x => x.Message).FirstOrDefault();
        }

        public static void ProcessDBException(DBConcurrencyException ex)
        {
            ProcessDBException(null, ex);
        }

        public static void ProcessDBException(Object owner, DBConcurrencyException ex)
        {
            //Abort();
            string message = "Данные изменены другим пользователем. Перезагрузите данные из базы и повторите попытку редактирования.";
#if DEBUG
            message = string.Format("{0}\n{1}", message, ex.Message);
#endif
            MessageUtility.ShowErrorMessage(owner, message);
        }

        public static void ProcessException(Object owner, System.Exception ex)
        {
            //Abort();

            var exceptionProcessed = false;
            ProcessException(owner, ex, out exceptionProcessed);

            if (exceptionProcessed)
                return;

        }

        private static void ProcessException(Object owner, System.Exception ex, out bool exceptionProcessed)
        {
            exceptionProcessed = false;
            SqlException sqlException;
            DBConcurrencyException dBConcurrencyException;
            if ((sqlException = ex as SqlException) != null)
            {
                ProcessSqlException(owner, sqlException);
                exceptionProcessed = true;
            }
            else if ((dBConcurrencyException = ex as DBConcurrencyException) != null)
            {
                ProcessDBException(owner, dBConcurrencyException);
                exceptionProcessed = true;
            }
            else if (ex.InnerException != null)
            {
                ProcessException(owner, ex.InnerException, out exceptionProcessed);
            }
        }
    }
}

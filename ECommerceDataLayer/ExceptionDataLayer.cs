namespace ECommerceDataLayer
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using ECommerceDataLayer.Extensions;

    public class ExceptionDataLayer
    {
        public void LogExceptionToDataBase(Exception exception)
        {
            using (SqlCommand command = new SqlCommand("Usp_ExceptionLogging_INS"))
            {
                command.Parameters.Add("@ExceptionMsg", SqlDbType.VarChar).Value = exception.Message.ToString();
                command.Parameters.Add("@ExceptionType", SqlDbType.VarChar).Value = exception.GetType().Name.ToString();
                command.Parameters.Add("@ExceptionSource", SqlDbType.VarChar).Value = exception.StackTrace.ToString();
                command.ExecuteQuery();
            }
        }
    }
}

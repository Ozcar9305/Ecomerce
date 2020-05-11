namespace ECommerceDataLayer
{
    using System;
    using System.Data.SqlClient;
    using ECommerceDataLayer.Extensions;

    public class ExceptionDataLayer
    {
        public void LogExceptionToDataBase(Exception exception)
        {
            using (SqlCommand command = new SqlCommand("Usp_ExceptionLogging_INS"))
            {
                command.Parameters.AddWithValue("@ExceptionMsg", exception.Message.ToString());
                command.Parameters.AddWithValue("@ExceptionType", exception.GetType().Name.ToString());
                command.Parameters.AddWithValue("@ExceptionURL", exception);
                command.Parameters.AddWithValue("@ExceptionSource", exception.StackTrace.ToString());
                command.ExecuteQuery();
            }
        }
    }
}

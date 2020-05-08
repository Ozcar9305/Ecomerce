using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDataLayer.Extensions
{
    using System.Data;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System;
    using System.Data.SqlClient;

    /// <summary>
    /// Clase de manejador de datos
    /// </summary>
    public static class DataBaseExtensions
    {
        private static string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ECommerceString"].ToString();

        /// <summary>
        /// Ejecuta un select en la base de datos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static List<T> Select<T>(this SqlCommand command, Func<IDataReader, T> projection)
        {
            var resultList = new List<T>();
            if (command != null)
            {
                command.Connection = new SqlConnection();
                command.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ECommerceConnection"].ToString();
                command.Connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    resultList.Add(projection(reader));
                }
                reader.Close();
            }
            return resultList;
        }

        /// <summary>
        /// Ejecuta la consulta a base sql y retorna un escalar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static T Escalar<T>(this SqlCommand command)
        {
            var reader = command?.ExecuteScalar().ToString();
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(reader);
        }

        /// <summary>
        /// Ejecuta un exec non query en la base de datos
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool ExecuteQuery(this SqlCommand command)
        {
            bool isQueryExecuted = default(bool);
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                command.Connection  = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                isQueryExecuted = command?.ExecuteNonQuery() != default(int);
            }
            return isQueryExecuted;
        }

        /// <summary>
        /// Obtiene el valor de un dato dado, metodo extension
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Get<T>(this IDataReader reader, string name)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                if (reader != null && converter != null && reader.GetOrdinal(name) >= default(int))
                {
                    return (T)converter.ConvertFromString(reader[name].ToString());
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }
        
    }
}

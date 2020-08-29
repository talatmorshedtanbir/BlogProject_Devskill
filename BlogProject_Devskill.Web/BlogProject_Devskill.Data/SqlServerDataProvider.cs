using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BlogProject_Devskill.Data
{
    public class SqlServerDataProvider<T> : IDisposable, ISqlServerDataProvider<T>
    {
        protected readonly SqlConnection _sqlConnection;

        public SqlServerDataProvider(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }

        public (IList<T> result, int total, int totalDisplay) GetData(string procedureName,
            IList<(string key, object value, bool isOut)> parameters)
        {
            var result = new List<T>();

            var command = new SqlCommand(procedureName, _sqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                if (!param.isOut)
                    command.Parameters.AddWithValue(param.key, param.value);
                else
                    command.Parameters.Add(new SqlParameter
                    {
                        Direction = System.Data.ParameterDirection.Output,
                        ParameterName = param.key,
                        SqlDbType = System.Data.SqlDbType.Int
                    });
            }

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var itemType = typeof(T);
                    var constructor = itemType.GetConstructor(new Type[] { });
                    var instance = constructor.Invoke(new object[] { });
                    var properties = itemType.GetProperties();
                    foreach (var property in properties)
                    {
                        property.SetValue(instance, reader[property.Name]);
                    }

                    result.Add((T)instance);
                }
            }

            int total = (int)command.Parameters["Total"].Value;
            var totalDisplay = (int)command.Parameters["TotalDisplay"].Value;

            return (result, total, totalDisplay);
        }

        public void Dispose()
        {
            if (_sqlConnection != null)
            {
                if (_sqlConnection.State == System.Data.ConnectionState.Open)
                    _sqlConnection.Close();

                _sqlConnection.Dispose();
            }
        }
    }
}

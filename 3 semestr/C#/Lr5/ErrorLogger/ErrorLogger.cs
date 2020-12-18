using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Lr2WindowsService;

namespace LoggerToDB
{
    public class ErrorLogger
    {
        private ConfigurationProvider prov = new ConfigurationProvider();
        private List<OptionsForDeserealizing> listOfLogs = new List<OptionsForDeserealizing>();

        public ErrorLogger()
        {
            listOfLogs = prov.GetOption();
        }

        

        public async Task WriteErrorsToDataBaseAsync(string errorMessage)
        {
            using (var connection = new SqlConnection(listOfLogs[0].connectionStringToErrors))
            {
                await connection.OpenAsync();
                SqlTransaction transaction = connection.BeginTransaction();
                var command = new SqlCommand("AddError", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Transaction = transaction;

                try
                {
                    var messageParam =  command.Parameters.AddWithValue("@Message", errorMessage);
                    var timeParam = command.Parameters.AddWithValue("@Time", DateTime.Now.ToString("s"));
                    var reader = command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

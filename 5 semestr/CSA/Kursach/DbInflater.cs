using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Windows.UI.Popups;
using Newtonsoft.Json;


namespace uwp_test
{
    public static class DbInflater
    {
        private static string connectionString = "server=localhost;user=root;database=csa_kursach;password=0000;";
        public static void DbInflate()
        {
            string json = "";
            MySqlConnection connection = new MySqlConnection(connectionString);
            using (StreamReader streamRead = new StreamReader("cities.json", Encoding.Default))
            {
                try
                {
                    string sqlCheck = "SELECT COUNT(*) FROM csa_kursach.cities";
                    MySqlCommand commandCheck = new MySqlCommand(sqlCheck, connection);
                    connection.Open();
                    object isTableFilled = commandCheck.ExecuteScalar();

                    if (Convert.ToInt32(isTableFilled) == 0)
                    {
                        json = streamRead.ReadToEnd();
                        var items = JsonConvert.DeserializeObject<List<Country>>(json);
                        foreach (var item in items)
                        {
                            foreach (Region it in item)
                            {
                                foreach (City i in it)
                                {
                                    string sql = "INSERT INTO csa_kursach.cities(name, latitude, longitude) VALUES('" + i.Name + "', "
                                                        + i.Lat.ToString().Replace(",", ".") + ", " + i.Lng.ToString().Replace(",", ".") + ")";

                                    MySqlCommand command = new MySqlCommand(sql, connection);

                                    command.ExecuteNonQuery();

                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static async void MessageBox(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}

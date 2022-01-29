using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using Windows.UI.Popups;

namespace uwp_test
{
    public class CityViewModel
    {
        //private string sql = "SELECT cities.id AS ID, cities.name AS Город, cities.latitude AS Широта, cities.longitude AS Долгота, cities.population AS Население FROM csa_kursach.cities";
        private string sql = "SELECT cities.id, cities.name, cities.latitude, cities.longitude, cities.population FROM csa_kursach.cities";
        private string connectionString = "server=localhost;user=root;database=csa_kursach;password=0000;";
        public DataTable citiesTable;
        private MySqlDataAdapter adapter;
        private MySqlConnection connection;
        public ObservableCollection<City> citiesInfo = new ObservableCollection<City>();

        public CityViewModel()
        {
            citiesTable = new DataTable();
            connection = new MySqlConnection(connectionString);
        }
        public void CreateCitiesTable()
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                adapter = new MySqlDataAdapter(command);
                adapter.Fill(citiesTable);
                /*using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        citiesInfo.Add(new City 
                        { 
                            Name = reader.GetString(1), 
                            Lat = reader.GetDouble(2),
                            Lng = reader.GetDouble(3)
                        });
                    }
                }*/
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
        public double GetCouples(double lng1, double lat1, double lng2, double lat2)
        {

            double R = 6371; // Радиус Земли

            double sLat1 = Math.Sin((Math.PI / 180) * lat1);
            double sLat2 = Math.Sin((Math.PI / 180) * lat2);
            double cLat1 = Math.Cos((Math.PI / 180) * lat1);
            double cLat2 = Math.Cos((Math.PI / 180) * lat2);
            double cLon = Math.Cos((Math.PI / 180) * lng1 - (Math.PI / 180) * lng2);

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return dist;
        }

        public double GetTime(double distance)
        {
            double averageSpeed = 90; // км/ч

            double time = distance / averageSpeed;

            return time;
        }

        public async void MessageBox(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}

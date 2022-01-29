using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Data;
using uwp_test.Models;
using uwp_test.PointCluster;

namespace uwp_test
{
    //localSettings.Values["connectionString"] = "server=localhost;user=root;database=csa_kursach;password=0000;";

    public class CityCombo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        private CityViewModel cityViewModel;
        private ClusterizationViewModel clusterViewModel;
        private List<double> pairs = new List<double>();
        //private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string connectionString = "server=localhost;user=root;database=csa_kursach;password=0000;";
        private ObservableCollection<int> comboBoxItems = new ObservableCollection<int>();

        private int loopStart = 0;

        private string buttonType = "";

        private MapPage mapPage = new MapPage();
        public static Dictionary<UIContext, AppWindow> AppWindows { get; set; }
            = new Dictionary<UIContext, AppWindow>();

        private List<Book> Books;
        private List<SubCluster> Clusters;
        //private Cluster cluster = new Cluster();
        private ObservableCollection<CityCombo> citiesC = new ObservableCollection<CityCombo>();
        private List<Cluster> clusters;
        public MainPage()
        {
            this.InitializeComponent();

            DbInflater.DbInflate();

            cityViewModel = new CityViewModel();
            cityViewModel.CreateCitiesTable();

            clusterViewModel = new ClusterizationViewModel();
            
            Books = BookManager.GetBooks(cityViewModel.citiesTable);

            for(int i = 0; i < cityViewModel.citiesTable.Rows.Count; i++)
            {
                citiesC.Add(new CityCombo()
                {
                    Id = Convert.ToInt32(cityViewModel.citiesTable.Rows[i].ItemArray.ElementAt(0)),
                    Name = cityViewModel.citiesTable.Rows[i].ItemArray.ElementAt(1).ToString()
                });                
            }
            cityComboBox.DataContext = citiesC;
            cityComboBox.IsEnabled = false;
        }

        private async void showMapButton_Click(object sender, RoutedEventArgs e)
        {
            AppWindow appWindow = await AppWindow.TryCreateAsync();

            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(AppWindowPage));
            AppWindowPage page = (AppWindowPage)appWindowContentFrame.Content;
            page.MyAppWindow = appWindow;

            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);

            UIContext uC = appWindowContentFrame.UIContext;
            AppWindows.Add(uC, appWindow);
            appWindow.Title = "App Window ";

            appWindow.Closed += delegate
            {
                MainPage.AppWindows.Remove(appWindowContentFrame.UIContext);
                appWindowContentFrame.Content = null;
                appWindow = null;
            };

            // Show the window.
            await appWindow.TryShowAsync();
        }

        private void distanceButton_Click(object sender, RoutedEventArgs e)
        {

            MySqlConnection connection = new MySqlConnection(connectionString);
            //int count = 0;
            try
            {
                string sqlCheck = "SELECT COUNT(*) FROM csa_kursach.clusterization_info";
                MySqlCommand commandCheck = new MySqlCommand(sqlCheck, connection);
                connection.Open();
                object isTableFilled = commandCheck.ExecuteScalar();
                connection.Close();

                if (Convert.ToInt32(isTableFilled) != 0)
                {
                    string sqlTr = "TRUNCATE TABLE clusterization_info";
                    MySqlCommand commandTr = new MySqlCommand(sqlTr, connection);
                    connection.Open();
                    commandTr.ExecuteNonQuery();
                    connection.Close();
                }
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();

                if (cityTypeCheckBox.IsChecked == false)
                {
                    for (int i = 0; i < cityViewModel.citiesTable.Rows.Count; i++)
                    {
                        loopStart = i;
                        var a = Parallel.For(i + 1, cityViewModel.citiesTable.Rows.Count, MakeCityCouples);
                    }
                }
                else
                {
                    int cityId = ((CityCombo)cityComboBox.SelectedItem).Id - 1;
                    loopStart = cityId;
                    for (int i = 0; i < cityViewModel.citiesTable.Rows.Count; i++)
                    {
                        MakeCityCouples(i);
                    }
                }
                stopwatch.Stop();
                MessageBox($"Время, затраченное на рассчет расстояния и время между городами и добавлений их в базу: {stopwatch.Elapsed}");
                
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

        public async void MessageBox(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        private void MakeCityCouples(int x)
        {
            double dist = 0;
            double time = 0;

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            double lat1 = Convert.ToDouble(cityViewModel.citiesTable.Rows[loopStart].ItemArray.ElementAt(2));
            double lat2 = Convert.ToDouble(cityViewModel.citiesTable.Rows[x].ItemArray.ElementAt(2));

            double lng1 = Convert.ToDouble(cityViewModel.citiesTable.Rows[loopStart].ItemArray.ElementAt(3));
            double lng2 = Convert.ToDouble(cityViewModel.citiesTable.Rows[x].ItemArray.ElementAt(3));

            int city_id_1 = Convert.ToInt32(cityViewModel.citiesTable.Rows[loopStart].ItemArray.ElementAt(0));
            int city_id_2 = Convert.ToInt32(cityViewModel.citiesTable.Rows[x].ItemArray.ElementAt(0));
            if (lat1 != 0 && lat2 != 0 && lng1 != 0 && lng2 != 0)
            {
                dist = cityViewModel.GetCouples(lng1, lat1, lng2, lat2);
                time = cityViewModel.GetTime(dist);

                string sqlDistance = $"INSERT INTO csa_kursach.clusterization_info (id_city_1, id_city_2, distance, time) " +
                    $"VALUES({city_id_1}, {city_id_2}, {dist.ToString().Replace(",", ".")}, {time.ToString().Replace(",", ".")})";

                MySqlCommand command = new MySqlCommand(sqlDistance, connection);
                command.ExecuteNonQuery();
                //pairs.Add(dist);
            }

            connection.Close();
        }

        private async void clusterizationButton_Click(object sender, RoutedEventArgs e)
        {
            buttonType = "clusterizationButton";

            MySqlConnection conn = new MySqlConnection(connectionString);

            string time = maxTimeTextBox.Text;
            //if (time < 0 || time > 10)
            //{
                //Присвоим стандартное значение
            //    time = 1;
            //}
            clusterViewModel.CreateClustersTable(time);
            
            string sqlCheck = "SELECT COUNT(*) FROM csa_kursach.clusterization_info";
            MySqlCommand commandCheck = new MySqlCommand(sqlCheck, conn);
            conn.Open();
            object isTableFilled = commandCheck.ExecuteScalar();
            conn.Close();

            if (Convert.ToInt32(isTableFilled) == 0)
            {
                MessageBox("Пожалуйста, перед тем как запускать алгоритм класетризации, убедитесь, что имеются для этого нужные данные! " +
                                "(Предварительно нажмите кнопку с рассчетом расстояния и время)");
                return;
            }
            else
            {

                string[] attributes = new string[] { "Время" };
                int numOfAttributes = attributes.Length;

                int numOfClusters = Convert.ToInt32(clusterNumberTextBox.Text);
                int maxCount = 30;

                double[][] data = new double[clusterViewModel.clustersTable.Rows.Count][];
                int[] id = new int[clusterViewModel.clustersTable.Rows.Count];
                double[] dataOne = new double[clusterViewModel.clustersTable.Rows.Count];

                List<Point> points = new List<Point>();


                for (int i = 0; i < clusterViewModel.clustersTable.Rows.Count; i++)
                {
                    data[i] = new double[] { Convert.ToDouble(clusterViewModel.clustersTable.Rows[i].ItemArray.ElementAt(4)) };
                    id[i] = Convert.ToInt32(clusterViewModel.clustersTable.Rows[i].ItemArray.ElementAt(0));
                    dataOne[i] = data[i][0];
                }
                List<double> test = dataOne.ToList();
                test.Sort();
                dataOne = test.ToArray();
                for (int i = 0; i < clusterViewModel.clustersTable.Rows.Count; i++)
                {
                    data[i][0] = dataOne[i];
                }
                

                var clusters =  clusterViewModel.Cluster(data, numOfClusters, numOfAttributes, maxCount);

                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

                try
                {
                    conn.Open();
                    stopwatch.Start();
                    for (int i = 0; i < clusters.Length; i++)
                    {
                        string sqlCluster = $"UPDATE csa_kursach.clusterization_info SET cluster_number = {clusters[i]} WHERE csa_kursach.clusterization_info.id = {id[i]}";
                        MySqlCommand command = new MySqlCommand(sqlCluster, conn);
                        await command.ExecuteNonQueryAsync();
                    }
                    stopwatch.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                FillComboBox();
                MessageBox($"Время на кластеризацию и добавление номеров кластеров в базу: {stopwatch.Elapsed}");
            }

        }

        private void FillComboBox()
        {
            int clusterNumber = 0;
            if (!string.IsNullOrEmpty(clusterNumberTextBox.Text))
            {
                comboBoxItems.Clear();
                clusterNumber = Convert.ToInt32(clusterNumberTextBox.Text);
                for (int i = 0; i < clusterNumber; i++)
                {
                    comboBoxItems.Add(i);
                }
                clusterNumberComboBox.SelectedItem = comboBoxItems.ElementAt(0);
                clusterNumberComboBox.ItemsSource = comboBoxItems;

            }
            else
            {
                clusterNumberComboBox.ItemsSource = "";
            }

        }



        private void clusterNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (clusterNumberComboBox.SelectedItem != null)
            {
                ComboBoxClicked(buttonType);
            }
        }

        private void ComboBoxClicked(string buttonName)
        {
            string sql = "SELECT c1.name AS name1, c2.name AS name2, distance, time " +
                          "FROM csa_kursach.clusterization_info INNER JOIN csa_kursach.cities c1 ON clusterization_info.id_city_1 = c1.id " +
                          $"INNER JOIN csa_kursach.cities c2 ON clusterization_info.id_city_2 = c2.id WHERE cluster_number = {clusterNumberComboBox.SelectedItem} " +
                          "ORDER BY csa_kursach.clusterization_info.time";
            
            string sqlPopulation = "SELECT c1.population " +
                                   "FROM csa_kursach.clusterization_info INNER JOIN csa_kursach.cities c1 ON clusterization_info.id_city_1 = c1.id " +
                                   $"WHERE cluster_number = {clusterNumberComboBox.SelectedItem} " +
                                   "GROUP BY c1.name";
            
            int populationSum = 0;

            MySqlConnection connect = new MySqlConnection(connectionString);

            try
            {
                connect.Open();

                MySqlCommand command = new MySqlCommand(sql, connect);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);


                MySqlCommand commandPopulation = new MySqlCommand(sqlPopulation, connect);
                MySqlDataAdapter populationAdapter = new MySqlDataAdapter(commandPopulation);
                DataTable populationTable = new DataTable();
                populationAdapter.Fill(populationTable);

                for (int i = 0; i < populationTable.Rows.Count; i++)
                {
                    populationSum += Convert.ToInt32(populationTable.Rows[i].ItemArray.ElementAt(0));
                }

                Clusters = ClusterManager.GetClusters(table);
                Bindings.Update();

                int a = clusterNumberComboBox.SelectedIndex;

                double centroid = 0;

                //Определим какая кнопка была нажата и вызовем нужные центроиды для вывода
                string s = "";
                if (buttonName == "clusterizationButton")
                {
                    centroid = clusterViewModel.mCentroid[a][0];
                    s = string.Format("{0:F4}", centroid);
                }

                if (buttonName == "clusterizationButtonTest")
                {
                    Point pP = clusters[a].GetBestCenter();
                    s = string.Format("{0:F4}", pP.X);
                }
                string s1 = string.Format("{0:### ### ###}", populationSum);
                clusterInfo.Text = $"Центроид кластера: {s}\nНаселение кластера: {s1}";
            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private async void clusterizationButtonTest_Click(object sender, RoutedEventArgs e)
        {

            buttonType = "clusterizationButtonTest";

            MySqlConnection conn = new MySqlConnection(connectionString);
            string time = maxTimeTextBox.Text;
            //if(time < 0 || time > 10)
            //{
                //Присвоим стандартное значение
            //    time = 2;
            //}
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            clusterViewModel.CreateClustersTable(time);

            List<Point> points = new List<Point>();

            int numOfClusters = Convert.ToInt32(clusterNumberTextBox.Text);

            for (int i = 0; i < clusterViewModel.clustersTable.Rows.Count; i++)
            {
                var point = new Point(Convert.ToDouble(clusterViewModel.clustersTable.Rows[i].ItemArray.ElementAt(4)),
                Convert.ToDouble(clusterViewModel.clustersTable.Rows[i].ItemArray.ElementAt(3)),
                Convert.ToInt32(clusterViewModel.clustersTable.Rows[i].ItemArray.ElementAt(0)));
                points.Add(point);
            }


            clusters = KMeans.Calculate(points, numOfClusters);

            conn.Open();

            for (int i = 0; i < clusters.Count; i++)
            {
                for (int j = 0; j < clusters[i].Points.Count; j++)
                {
                    string sqlCluster = $"UPDATE csa_kursach.clusterization_info SET cluster_number = {i} WHERE id = {clusters[i].Points[j].ID}";
                    MySqlCommand command = new MySqlCommand(sqlCluster, conn);
                    int a = await command.ExecuteNonQueryAsync();
                }
            }

            conn.Close();
            stopwatch.Stop();

            FillComboBox();

            MessageBox($"Время на кластеризацию и добавление номеров кластеров в базу: {stopwatch.Elapsed}");
        }

        private void cityTypeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cityComboBox.IsEnabled = true;
        }

        private void cityTypeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            cityComboBox.IsEnabled = false;
        }
    }
}

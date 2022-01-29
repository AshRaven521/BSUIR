using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.WindowManagement;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace uwp_test
{
    public sealed partial class AppWindowPage : Page
    {
        AppWindow window;
        public int combo = 0;
        private ObservableCollection<int> comboBoxItems = new ObservableCollection<int>();

        public AppWindow MyAppWindow { get; set; }

        List<MapElement> Highlights = new List<MapElement>();
        List<MapElement> Lines = new List<MapElement>();
        public AppWindowPage()
        {
            this.InitializeComponent();

            Loaded += AppWindowPage_Loaded;
        }

        private void AppWindowPage_Loaded(object sender, RoutedEventArgs e)
        {
            window = MainPage.AppWindows[this.UIContext];

            window.Changed += Window_Changed;

            NavigatedTo();

            for (int i = 0; i < 10; i++)
            {
                comboBoxItems.Add(i);
            }
            cluster.SelectedItem = comboBoxItems.ElementAt(0);
            cluster.ItemsSource = comboBoxItems;
            //ComboBoxClicked();
            //DrawLineOnMap(53.9284, 27.5204, 53.1120, 25.3192);
        }

        private void Window_Changed(AppWindow sender, AppWindowChangedEventArgs args)
        {

        }

        private void NavigatedTo()
        {
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 53.9284, Longitude = 27.5204 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            MapControl1.Center = cityCenter;
            MapControl1.ZoomLevel = 7;
            MapControl1.LandmarksVisible = true;
        }

        public void DrawLineOnMap(double lat1, double lon1, double lat2, double lon2)
        {


            Color cc = Colors.Red;
            switch (combo)
            {
                case 1: cc = Colors.Purple; break;
                case 2: cc = Colors.Green; break;
                case 3: cc = Colors.Yellow; break;
                case 4: cc = Colors.Blue; break;
                case 5: cc = Colors.DeepPink; break;
            }

            var mapPolyline = new MapPolyline
            {
                Path = new Geopath(new List<BasicGeoposition> {
                    new BasicGeoposition() {Latitude=lat1, Longitude=lon1 },
                    new BasicGeoposition() {Latitude=lat2, Longitude=lon2 },
                }),
                StrokeColor = cc,
                StrokeThickness = 2,
                StrokeDashed = false,
            };

            Lines.Add(mapPolyline);
        }

        public void HighlightArea(double lat1, double lon1, double lat2, double lon2)
        {
            Color cc = Colors.Red;
            switch (combo)
            {
                case 1: cc = Colors.Purple; break;
                case 2: cc = Colors.Green; break;
                case 3: cc = Colors.Yellow; break;
                case 4: cc = Colors.Blue; break;
                case 5: cc = Colors.DeepPink; break;
            }
            var mapPolygon = new MapPolygon
            {
                Path = new Geopath(new List<BasicGeoposition> {
                    new BasicGeoposition() {Latitude=lat1+0.02, Longitude=lon1-0.05 },
                    new BasicGeoposition() {Latitude=lat1-0.02, Longitude=lon1-0.05 },
                    new BasicGeoposition() {Latitude=lat1-0.02, Longitude=lon1+0.05 },
                    new BasicGeoposition() {Latitude=lat1+0.02, Longitude=lon1+0.05 },
                }),
                ZIndex = 1,
                FillColor = cc,
                StrokeColor = Colors.Blue,
                StrokeThickness = 1,
                StrokeDashed = false,
            };

            var mapPolygon2 = new MapPolygon
            {
                Path = new Geopath(new List<BasicGeoposition> {
                    new BasicGeoposition() {Latitude=lat2+0.02, Longitude=lon2-0.05 },
                    new BasicGeoposition() {Latitude=lat2-0.02, Longitude=lon2-0.05 },
                    new BasicGeoposition() {Latitude=lat2-0.02, Longitude=lon2+0.05 },
                    new BasicGeoposition() {Latitude=lat2+0.02, Longitude=lon2+0.05 },
                }),
                ZIndex = 1,
                FillColor = cc,
                StrokeColor = Colors.Blue,
                StrokeThickness = 1,
                StrokeDashed = false,
            };


            Highlights.Add(mapPolygon);
            Highlights.Add(mapPolygon2);
        }

        private void ComboBoxClicked()
        {
            string sql = "SELECT c1.latitude AS lat1, c1.longitude AS lon1, c2.latitude AS lat2, c2.longitude AS lon2 " +
                          "FROM csa_kursach.clusterization_info INNER JOIN csa_kursach.cities c1 ON clusterization_info.id_city_1 = c1.id " +
                          $"INNER JOIN csa_kursach.cities c2 ON clusterization_info.id_city_2 = c2.id WHERE cluster_number = {combo} " +
                          "ORDER BY csa_kursach.clusterization_info.time";

            MySqlConnection connect = new MySqlConnection("server=localhost;user=root;database=csa_kursach;password=0000;");

            try
            {
                connect.Open();

                MySqlCommand command = new MySqlCommand(sql, connect);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                int count = table.Rows.Count;

                int layCount = MapControl1.Layers.Count;
                for (int j = 0; j < layCount; j++)
                {
                    MapControl1.Layers[j].Visible = false;

                }

                int pointsForDrawing = Convert.ToInt32(pointNumberTextBox.Text);
                if (pointsForDrawing < 2 || pointsForDrawing > 10000)
                {
                    pointsForDrawing = 100;
                }
                if (pointsForDrawing > count)
                    pointsForDrawing = count;

                Lines.Clear();
                Highlights.Clear();
                for (int i = 0; i < pointsForDrawing; i++)
                {
                    double lat1 = Convert.ToDouble(table.Rows[i].ItemArray[0]);
                    double lon1 = Convert.ToDouble(table.Rows[i].ItemArray[1]);
                    double lat2 = Convert.ToDouble(table.Rows[i].ItemArray[2]);
                    double lon2 = Convert.ToDouble(table.Rows[i].ItemArray[3]);

                    if (viewTypeComboBox.SelectedIndex == 0)
                    {
                        DrawLineOnMap(lat1, lon1, lat2, lon2);
                    }
                    else
                    {
                        HighlightArea(lat1, lon1, lat2, lon2);
                    }
                }
                if (viewTypeComboBox.SelectedIndex == 0)
                {
                    var LinesLayer = new MapElementsLayer
                    {
                        ZIndex = 1,
                        MapElements = Lines
                    };

                    MapControl1.Layers.Add(LinesLayer);
                }
                else
                {
                    var HighlightsLayer = new MapElementsLayer
                    {
                        ZIndex = 1,
                        MapElements = Highlights
                    };
                    MapControl1.Layers.Add(HighlightsLayer);
                }
            }
            catch (Exception ex)
            {
                //MessageBox(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private void cluster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private async void applyButton_Click(object sender, RoutedEventArgs e)
        {
            combo = Convert.ToInt32(cluster.SelectedIndex);
            ComboBoxClicked();
        }
    }
}

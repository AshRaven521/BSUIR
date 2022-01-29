using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace uwp_test
{
    public class ClusterizationViewModel
    {
        private string sql = "SELECT clusterization_info.id AS ID, clusterization_info.id_city_1 AS Город№1, clusterization_info.id_city_2 AS Город№2, " +
                             "clusterization_info.distance AS Расстояние, clusterization_info.time AS Время FROM csa_kursach.clusterization_info";
        private string connectionString = "server=localhost;user=root;database=csa_kursach;password=0000;";
        public DataTable clustersTable;
        private MySqlDataAdapter adapter;
        private MySqlConnection connection;
        public double[][] mCentroid;

        public ClusterizationViewModel()
        {
            clustersTable = new DataTable();
            connection = new MySqlConnection(connectionString);
        }

        public void CreateClustersTable(string timeLimiter)
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "."};
            double timeD = double.Parse(timeLimiter, formatter);
            
            string sql2 = "SELECT clusterization_info.id AS ID, clusterization_info.id_city_1 AS Город№1, clusterization_info.id_city_2 AS Город№2, " +
                             "clusterization_info.distance AS Расстояние, clusterization_info.time AS Время FROM csa_kursach.clusterization_info " +
                            $"WHERE time < " + timeLimiter;
                        
            try
            {
                connection.Open();
                clustersTable = new DataTable();
                MySqlCommand command = new MySqlCommand(sql2, connection);
                adapter = new MySqlDataAdapter(command);
                adapter.Fill(clustersTable);
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

        public async Task<int[]> ClusterAsync(double[][] rawData, int numClusters, int numAttributes, int maxCount)
        {
            return await Task.Run(() => Cluster(rawData, numClusters, numAttributes, maxCount));
        }

        public int[] Cluster(double[][] rawData, int numClusters, int numAttributes, int maxCount)
        {
            bool changed = true;
            int ct = 0;
            int numTuples = rawData.Length;
            int[] clustering = InitClustering(numTuples, numClusters, 0);
            double[][] means = Allocate(numClusters, numAttributes);
            double[][] centroids = Allocate(numClusters, numAttributes);
            mCentroid = Allocate(numClusters, numAttributes);
            UpdateMeans(rawData, clustering, means);
            UpdateCentroids(rawData, clustering, means, centroids);
            while (changed == true && ct < maxCount) // maxcount - сколько шагов алгоритм делает на уточнение центроида
            {
                ++ct;
                changed = Assign(rawData, clustering, centroids);
                UpdateMeans(rawData, clustering, means);
                UpdateCentroids(rawData, clustering, means, centroids);
            }
            return clustering;
        }

        private void UpdateMeans(double[][] rawData, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            for (int k = 0; k < means.Length; ++k)
            {
                for (int j = 0; j < means[k].Length; ++j)
                {
                    means[k][j] = 0.0;
                }
            }
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
                for (int j = 0; j < rawData[i].Length; ++j)
                {
                    means[cluster][j] += rawData[i][j];
                }
            }
            for (int k = 0; k < means.Length; ++k)
            {
                for (int j = 0; j < means[k].Length; ++j)
                {
                    if (clusterCounts[k] != 0)
                    {
                        means[k][j] /= clusterCounts[k];
                    }
                    else
                    {
                        throw new DivideByZeroException("На ноль делить нельзя!");
                    }
                }
            }
            return;
        }

        private double[][] Allocate(int numClusters, int numAttributes)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
            {
                result[k] = new double[numAttributes];
            }
            return result;
        }

        private double[] ComputeCentroid(double[][] rawData, int[] clustering, int cluster, double[][] means)
        {
            int numAttributes = means[0].Length;
            double[] centroid = new double[numAttributes];
            double minDist = double.MaxValue;
            for (int i = 0; i < rawData.Length; ++i) // Перебираем каждую последовательность данных
            {
                int c = clustering[i];
                if (c != cluster)
                {
                    continue;
                }
                double currDist = Distance(rawData[i], means[cluster]);
                if (currDist < minDist)
                {
                    minDist = currDist;
                    for (int j = 0; j < centroid.Length; ++j)
                    {
                        centroid[j] = rawData[i][j];
                    }
                }
            }
            return centroid;
        }

        private void UpdateCentroids(double[][] rawData, int[] clustering, double[][] means, double[][] centroids)
        {
            for (int k = 0; k < centroids.Length; ++k)
            {
                double[] centroid = ComputeCentroid(rawData, clustering, k, means);
                centroids[k] = centroid;
                mCentroid[k] = centroids[k];
            }
        }

        private double Distance(double[] tuple, double[] vector)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
            {
                sumSquaredDiffs += Math.Pow((tuple[j] - vector[j]), 2);
            }
            return Math.Sqrt(sumSquaredDiffs);
        }

        private bool Assign(double[][] rawData, int[] clustering, double[][] centroids)
        {
            int numClusters = centroids.Length;
            bool changed = false;
            double[] distances = new double[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                {
                    distances[k] = Distance(rawData[i], centroids[k]);
                }
                int newCluster = MinIndex(distances);
                if (newCluster != clustering[i])
                {
                    changed = true;
                    clustering[i] = newCluster;
                }
            }
            return changed;
        }

        private int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k]; indexOfMin = k;
                }
            }
            return indexOfMin;
        }

        private int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i)
            {
                clustering[i] = i;
            }
            for (int i = numClusters; i < clustering.Length; ++i)
            {
                clustering[i] = random.Next(0, numClusters);
            }
            return clustering;
        }

        public async void MessageBox(string message)
        {
            var messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}

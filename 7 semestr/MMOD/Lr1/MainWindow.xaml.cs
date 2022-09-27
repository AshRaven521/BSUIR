using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Lr1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const int length = 1000000;
        public MainWindow()
        {
            InitializeComponent();
        }

        private double[] GenerateList()
        {
            double[] arr = new double[length];
            Random rand = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.NextDouble();
            }
            return arr;
        }

        private double GetActualProbability(double probability)
        {
            var collection = GenerateList();
            var neededElements = collection.Where(val => val < probability).ToList();
            double neededProbability = neededElements.Count / (double)length;
            return neededProbability;
        }

        private List<double> ParseToDoubleArray(string[] strArray)
        {
            double p = 0.0;
            List<double> probs = new List<double>();
            foreach (var s in strArray)
            {
                bool isConverted = double.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out p);
                if (isConverted == false)
                {
                    MessageBox.Show("Значение должно быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }
                if (p < 0 || p > 1)
                {
                    MessageBox.Show("Вероятность должна быть в диапазоне от 0 до 1", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                probs.Add(p);
            }

            return probs;
        }

        private void task1Button_Click(object sender, RoutedEventArgs e)
        {
            double p = 0.0;
            bool isConvertable = double.TryParse(task1TextBox.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out p);
            if (isConvertable == false)
            {
                MessageBox.Show("Значение должно быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (p < 0 || p > 1)
            {
                MessageBox.Show("Вероятность должна быть в диапазоне от 0 до 1", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double prob = GetActualProbability(p);
            task1TextBox.Text = prob.ToString();
        }

        private void task2Button_Click(object sender, RoutedEventArgs e)
        {
            var probsStr = task2TextBox.Text.Trim().Split(',');
            var probs = ParseToDoubleArray(probsStr);
            var neededProbs = new List<double>();
            foreach (var item in probs)
            {
                double p = GetActualProbability(item);
                neededProbs.Add(p);
            }
            task2ProbabilitiesList.ItemsSource = neededProbs;
            task2ProbabilitiesList.Visibility = Visibility.Visible;
        }

        private void task3Button_Click(object sender, RoutedEventArgs e)
        {
            double prob_a = 0.0;
            bool isFirstConverted = double.TryParse(task3TextBoxA.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out prob_a);

            double prob_b_a = 0.0;
            bool isSecondConverted = double.TryParse(task3TextBoxB.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out prob_b_a);

            if (isFirstConverted == false || isSecondConverted == false)
            {
                MessageBox.Show("Значение должно быть числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (prob_a < 0 || prob_a > 1 || prob_b_a < 0 || prob_b_a > 1)
            {
                MessageBox.Show("Вероятность должна быть в диапазоне от 0 до 1", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double prob_not_a = 1 - prob_a;

            List<string> results = new List<string>();

            Dictionary<string, double> expectedValues = new Dictionary<string, double>
            {
                { "p(AB)", prob_a* prob_b_a },
                { "p(A!B)", prob_not_a* (1 - prob_b_a) },
                { "p(!AB)", (1 - prob_b_a) * prob_a },
                { "p(!A!B)", prob_b_a * prob_not_a }
            };

            Dictionary<string, double> actualValues = new Dictionary<string, double>
            {
                {"p(AB)", 0 },
                {"p(A!B)", 0 },
                {"p(!AB)", 0 },
                {"p(!A!B)", 0 }
            };


            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                if (rand.NextDouble() < prob_a)
                {
                    string key = rand.NextDouble() < prob_b_a ? "p(AB)" : "p(!AB)";
                    actualValues[key]++;
                }
                else
                {
                    string key = rand.NextDouble() < prob_b_a ? "p(!A!B)" : "p(A!B)";
                    actualValues[key]++;
                }
            }

            string expectedRes = string.Empty;
            results.Add("Ожидаемый результат:");

            foreach (var res in expectedValues)
            {
                expectedRes += $"{res.Key} : {res.Value}\n";
            }
            results.Add(expectedRes);

            string actualRes = string.Empty;
            results.Add("Итоговый результат:");
            foreach (var res in actualValues)
            {
                actualRes += $"{res.Key} : {res.Value / length}\n";
            }
            results.Add(actualRes);

            task3ResultListBox.ItemsSource = results;
            task3ResultListBox.Visibility = Visibility.Visible;
        }

        private void task4Button_Click(object sender, RoutedEventArgs e)
        {
            var probsStr4 = task4TextBox.Text.Trim().Split(',');

            var probs4 = ParseToDoubleArray(probsStr4);

            List<double> resList = new List<double>();

            double[] actualProbs = new double[probsStr4.Length];
            for (int i = 0; i < actualProbs.Length; i++)
            {
                actualProbs[i] = 0;
            }

            var cumulativeArray = new List<double>();
            //Копируем список probs в список cumulativeArray
            for (int i = 0; i < probs4.Count; i++)
            {
                cumulativeArray.Add(probs4[i]);
            }
            //Кумулятивная сумма
            for (int i = 1; i < cumulativeArray.Count; i++)
            {
                cumulativeArray[i] = cumulativeArray[i] + cumulativeArray[i - 1];
            }

            double[] randomArray = GenerateList();
            foreach (var rand in randomArray)
            {
                int index = cumulativeArray.FindIndex(sum => rand < sum);
                if (index == -1)
                {
                    MessageBox.Show("Не нашлось такого элемента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    actualProbs[index]++;
                }
            }

            foreach (var prob in actualProbs)
            {
                resList.Add(prob / length);
            }

            task4ResultListBox.ItemsSource = resList;
            task4ResultListBox.Visibility = Visibility.Visible;
        }
    }
}

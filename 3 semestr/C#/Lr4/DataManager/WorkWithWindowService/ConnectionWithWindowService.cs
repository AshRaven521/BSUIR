using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Windows;

namespace DataManager.WorkWithWindowService
{
    public class ConnectionWithWindowService
    {
        private static string service = "Lr2 Windows Service";

        private static ServiceController controller = new ServiceController(service);

        public void StartService()
        {
            if (controller.Status != ServiceControllerStatus.Running)
            {
                controller.Start();
                controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
                MessageBox.Show("Успешно запущено","Запуск",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Уже запущено","Запуск",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        public void StopService()
        {
            if (controller.Status != ServiceControllerStatus.Stopped)
            {
                controller.Stop();
                controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                MessageBox.Show("Успешно остановлено","Стоп",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Уже остановлено","Стоп",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void ShowMessage(string eventDescription, string serviceEvent)
        {
            MessageBox.Show($"The service {eventDescription}", $"{serviceEvent}");

        }
    }
}

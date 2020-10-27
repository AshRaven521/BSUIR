using System;
using System.ServiceProcess;
using System.Windows;
namespace Lr2WpfApplication
{
    class ConnectionWithWindowService
    {
        private static string service = "Lr2 Windows Service";

        private static ServiceController controller = new ServiceController(service);

        public void StartService()
        {
            if (controller.Status != ServiceControllerStatus.Running)
            {
                controller.Start();
                controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMinutes(1));
                MessageBox.Show("was succesfully launched");
            }
            else
            {
                MessageBox.Show("is already running");
            }
        }

        public  void StopService()
        {
            if (controller.Status != ServiceControllerStatus.Stopped)
            {
                controller.Stop();
                controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(1));
                MessageBox.Show("was successfully stopped");
            }
            else
            {
                MessageBox.Show("is already stopped");
            }
        }

        private void ShowMessage(string eventDescription, string serviceEvent)
        {
            MessageBox.Show($"The service {eventDescription}", $"{serviceEvent}");
        }
    }
}

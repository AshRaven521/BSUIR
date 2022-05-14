using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Kursach_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PerformanceCounter cpuCounter;
        private PerformanceCounter memoryCounter;
        private Dictionary<string, List<Process>> processes = new Dictionary<string, List<Process>>();

        DispatcherTimer memoryTimer = new DispatcherTimer();
        DispatcherTimer cpuTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            memoryCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");

            memoryTimer.Interval = TimeSpan.FromSeconds(1);
            memoryTimer.Tick += MemoryTimer_Tick;

            cpuTimer.Interval = TimeSpan.FromSeconds(1);
            cpuTimer.Tick += CpuTimer_Tick;
        }

        private void CpuTimer_Tick(object sender, EventArgs e)
        {
            showCPUUsageButton_Click(null, null);
        }

        private void MemoryTimer_Tick(object sender, EventArgs e)
        {
            showMemoryUsageButton_Click(null, null);
        }

        private void showProcessesButton_Click(object sender, RoutedEventArgs e)
        {
            var procList = new List<string>();
            procList.Clear();
            processes.Clear();
            processesListBox.Items.Clear();

            foreach (var winProc in Process.GetProcesses())
            {
                if (!processes.ContainsKey(winProc.ProcessName))
                {
                    processes.Add(winProc.ProcessName, new List<Process>());
                    processes[winProc.ProcessName].Add(winProc);
                }
                else
                {
                    processes[winProc.ProcessName].Add(winProc);
                }
            }

            foreach (var processname in processes.Keys)
            {
                procList.Add(processname);
            }
            foreach (var item in procList)
            {
                processesListBox.Items.Add(item);
            }
            processesListBox.Visibility = Visibility.Visible;
        }

        private void showMemoryUsageButton_Click(object sender, RoutedEventArgs e)
        {
            memoryUsageTextBox.Visibility = Visibility.Visible;
            memoryUsageLabel.Visibility = Visibility.Visible;
            memoryUsageTextBox.Text = memoryCounter.NextValue().ToString() + " %";
            memoryTimer.Start();
        }

        private void showCPUUsageButton_Click(object sender, RoutedEventArgs e)
        {
            cpuUsageLabel.Visibility = Visibility.Visible;
            cpuUsageTextBox.Visibility = Visibility.Visible;
            cpuUsageTextBox.Text = cpuCounter.NextValue().ToString() + " %";
            cpuTimer.Start();
        }

        private void processesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(processesListBox.SelectedItem.ToString(), "Имя процесса", MessageBoxButton.OK, MessageBoxImage.Information);
            subProcessesListBox.Items.Clear();
            var myproc = processes[processesListBox.SelectedItem.ToString()];
            foreach (var proc in myproc)
            {
                // 1MB = 1024KB = 1024 * 1024
                subProcessesListBox.Items.Add(string.Format($"Process: {proc.Id}, Memory: {proc.PagedMemorySize64 / 1048576} MB"));
            }

            subProcessesListBox.Visibility = Visibility.Visible;
            killProcessesButton.Visibility = Visibility.Visible;
            killSingleProcessButton.Visibility = Visibility.Visible;
        }

        private void killProcessesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myproc = processes[processesListBox.SelectedItem.ToString()];
                foreach (var proc in myproc)
                {
                    proc.Kill();
                }
                showProcessesButton_Click(null, null);
                MessageBox.Show("Процессы завершены", "Завершение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nИзвините, это могут быть процессы Windows недоступные к завершению...", "Невозможность завершить процессы", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nИзвините, кажется, что-то пошло не по плану...", "Исключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void killSingleProcessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myproc = processes[processesListBox.SelectedItem.ToString()];
                if (subProcessesListBox.SelectedIndex != -1)
                {
                    myproc[subProcessesListBox.SelectedIndex].Kill();
                    showProcessesButton_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Выберите процесс!", "Процесс не выбран", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nИзвините, это может быть процесс Windows недоступный к завершению...", "Невозможность завершить процесс", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nИзвините, кажется, что-то пошло не по плану...", "Исключение", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void showConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            string computer = @"root\cimv2";

            string partOSQuery = "Win32_OperatingSystem";


            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;

            ManagementScope osScope = new ManagementScope(computer, options);
            ObjectQuery osQuery = new ObjectQuery($"SELECT * FROM {partOSQuery}");

            ManagementObjectSearcher osSearcher = new ManagementObjectSearcher(osScope, osQuery);

            ManagementObjectSearcher logicalDisk = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");
            ManagementObjectSearcher bios = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher disk = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher battery = new ManagementObjectSearcher("SELECT * FROM Win32_Battery");
            ManagementObjectSearcher cacheMemory = new ManagementObjectSearcher("SELECT * FROM Win32_CacheMemory");
            ManagementObjectSearcher usbHubs = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub");

            foreach (var osObj in osSearcher.Get())
            {
                configListBox.Items.Add("Конфигурация системы:");
                configListBox.Items.Add(string.Format($"Имя компьютера : {osObj["csname"]}"));
                configListBox.Items.Add(string.Format($"Директория Windows : {osObj["WindowsDirectory"]}"));
                configListBox.Items.Add(string.Format($"Операционная система : {osObj["Caption"]}"));
                configListBox.Items.Add(string.Format($"Версия : {osObj["Version"]}"));
                configListBox.Items.Add(string.Format($"Производитель : {osObj["Manufacturer"]}\n"));

            }

            foreach (var o in logicalDisk.Get())
            {
                configListBox.Items.Add("Конфигурация логического диска:");
                configListBox.Items.Add(string.Format($"Диск : {o["DeviceID"]}"));
                configListBox.Items.Add(string.Format($"Размер диска : {Convert.ToInt64(o["Size"].ToString()) / 1048576} MB\n"));
            }

            foreach (var b in bios.Get())
            {
                configListBox.Items.Add("Конфигурация BIOS:");
                configListBox.Items.Add(string.Format($"Версия BIOS : {b["SoftwareElementID"]}"));
                configListBox.Items.Add(string.Format($"Статус BIOS : {b["Status"]}\n"));
            }

            foreach (var dis in disk.Get())
            {
                configListBox.Items.Add("Конфигурация физического накопителя:");
                configListBox.Items.Add(string.Format($"Описание : {dis["Description"]}"));
                configListBox.Items.Add(string.Format($"ID устройства : {dis["DeviceID"]}"));
                configListBox.Items.Add(string.Format($"Размер : {Convert.ToInt64(dis["Size"]) / 1048576} MB\n"));
            }

            foreach (var bat in battery.Get())
            {
                configListBox.Items.Add("Конфигурация батареи компьютера:");
                configListBox.Items.Add(string.Format($"Описание : {bat["Description"]}"));
                configListBox.Items.Add(string.Format($"ID устройства : {bat["DeviceID"]}\n"));
            }

            foreach (var cache in cacheMemory.Get())
            {
                configListBox.Items.Add($"Конфигурация кэш-памяти:");
                configListBox.Items.Add(string.Format($"Уровень : {cache["Level"]}"));
                configListBox.Items.Add(string.Format($"ID устройства : {cache["DeviceID"]}"));
                configListBox.Items.Add(string.Format($"Статус : {cache["StatusInfo"]}"));
                configListBox.Items.Add(string.Format($"Размер : {cache["BlockSize"]} Bytes"));
                configListBox.Items.Add(string.Format($"Цель : {cache["Purpose"]}"));
                configListBox.Items.Add(string.Format($"Тип кэша : {cache["CacheType"]}\n"));
            }

            foreach (var usb in usbHubs.Get())
            {
                configListBox.Items.Add($"Конфигурация подключенных устройств:");
                configListBox.Items.Add(string.Format($"Название подключенного устройства : {usb["Name"]}"));
                configListBox.Items.Add(string.Format($"ID подключенного устройства : {usb["DeviceID"]}"));
                configListBox.Items.Add(string.Format($"Имя системы : {usb["SystemName"]}"));
            }
        }
    }
}

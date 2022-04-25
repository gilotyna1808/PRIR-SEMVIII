using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Aplikacja_Kliencka
{
    /// <summary>
    /// Logika interakcji dla klasy ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        ConfigClient _config;
        public ConfigWindow(ref ConfigClient config)
        {
            _config = config;
            InitializeComponent();
            LoadTxtBox();
        }

        private void LoadTxtBox()
        {
            txt_userName.Text = _config.RabitMQ_UserName;
            txt_password.Text = _config.RabitMQ_Password;
            txt_vHostName.Text = _config.RabitMQ_VirualHost;
            txt_hostName.Text = _config.RabitMQ_HostName;
            txt_port.Text = _config.RabitMQ_Port.ToString();
            txt_queue.Text = _config.RabitMQ_QueueRecive;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            string rabbitMQUser, rabbitMQOPass, rabbitMQVHost, rabbitMQHost, rabbitMQQueueR, rabitMQPort;
            rabbitMQUser = txt_userName.Text;
            rabbitMQOPass = txt_password.Text;
            rabbitMQVHost = txt_vHostName.Text;
            rabbitMQHost = txt_hostName.Text;
            rabbitMQQueueR = txt_queue.Text;
            rabitMQPort = txt_port.Text;
            try
            {
                _config.RabitMQ_UserName = rabbitMQUser;
                _config.RabitMQ_Password = rabbitMQOPass;
                _config.RabitMQ_VirualHost = rabbitMQVHost;
                _config.RabitMQ_HostName = rabbitMQHost;
                _config.RabitMQ_QueueRecive = rabbitMQQueueR;
                _config.RabitMQ_Port = int.Parse(rabitMQPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zła liczba klientow");
            }
        }

        private void btn_saveFile_Click(object sender, RoutedEventArgs e)
        {
            this.btn_save_Click(sender, e);
            _config.ZapiszKonfiguracje();
        }
    }
}

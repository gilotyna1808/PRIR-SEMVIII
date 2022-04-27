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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aplikacja_Kliencka
{
    /// <summary>
    /// Logika interakcji dla klasy ConfigConectionBoard.xaml
    /// </summary>
    public partial class ConfigConectionBoard : UserControl
    {
        public string RabbitName { get; set; }
        public string RabbitPassword { get; set; }
        public string RabbitVHost { get; set; }
        public string RabbitHost { get; set; }
        public int RabbitPort { get; set; }
        public bool Changes { get; set; }
        public ConfigConectionBoard(ConfigClient config)
        {
            InitializeComponent();
            Changes = false;
            RabbitName = config.RabitMQ_UserName;
            RabbitPassword = config.RabitMQ_Password;
            RabbitVHost = config.RabitMQ_VirualHost;
            RabbitHost = config.RabitMQ_HostName;
            RabbitPort = config.RabitMQ_Port;
            LoadTxtBox();
        }

        private void LoadTxtBox()
        {
            txt_userName.Text = RabbitName;
            txt_password.Text = RabbitPassword;
            txt_vHostName.Text = RabbitVHost;
            txt_hostName.Text = RabbitHost;
            txt_port.Text = RabbitPort.ToString();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Changes = true;
        }

        public void SaveChanges(ref ConfigClient config)
        {
            if (Changes)
            {
                config.RabitMQ_UserName = txt_userName.Text;
                config.RabitMQ_Password = txt_password.Text;
                config.RabitMQ_VirualHost = txt_vHostName.Text;
                config.RabitMQ_HostName = txt_hostName.Text;
                try
                {
                    int port = int.Parse(txt_port.Text);
                    config.RabitMQ_Port = port;
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

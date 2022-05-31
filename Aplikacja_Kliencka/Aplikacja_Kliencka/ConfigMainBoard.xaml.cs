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
    /// Logika interakcji dla klasy ConfigMainBoard.xaml
    /// </summary>
    public partial class ConfigMainBoard : UserControl
    {
        ConfigClient _config;
        Dictionary<string, UserControl> userControls = new Dictionary<string, UserControl>();
        public ConfigMainBoard(ref ConfigClient config)
        {
            _config = config;
            InitializeComponent();
            userControls.Add("conection", new ConfigConectionBoard(_config));
            userControls.Add("queue", new ConfigQueueBoard(_config));
            userControls.Add("client", new ConfigClientBoard(_config));
            userControls.Add("task", new ConfigTaskBoard(_config));
            this.btn_connectionConf_Click();
        }

        private void btn_connectionConf_Click(object sender=null, RoutedEventArgs e=null)
        {
            bool keyExists = userControls.ContainsKey("conection");
            if (keyExists)
            {
                ChangeConfigPage(userControls["conection"]);
            }
            else
            {
                userControls.Add("conection", new ConfigConectionBoard(_config));
                ChangeConfigPage(userControls["conection"]);
            }
            SetButtons(btn_connectionConf);
        }

        private void ChangeConfigPage(UserControl page)
        {
            grid_config_board.Children.Clear();
            grid_config_board.Children.Add(page);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("conection");
            if (keyExists)
            {
                var card = (ConfigConectionBoard)userControls["conection"];
                card.SaveChanges(ref _config);
            }
            keyExists = userControls.ContainsKey("queue");
            if (keyExists)
            {
                var card = (ConfigQueueBoard)userControls["queue"];
                card.SaveChanges(ref _config);
            }
            keyExists = userControls.ContainsKey("client");
            if (keyExists)
            {
                var card = (ConfigClientBoard)userControls["client"];
                card.SaveChanges(ref _config);
            }
            keyExists = userControls.ContainsKey("task");
            if (keyExists)
            {
                var card = (ConfigTaskBoard)userControls["task"];
                card.SaveChanges(ref _config);
            }
        }

        private void btn_queueConf_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("queue");
            if (keyExists)
            {
                ChangeConfigPage(userControls["queue"]);
            }
            else
            {
                userControls.Add("queue", new ConfigQueueBoard(_config));
                ChangeConfigPage(userControls["queue"]);
            }
            SetButtons(btn_queueConf);
        }

        private void btn_saveToFile_Click(object sender, RoutedEventArgs e)
        {
            this.btn_save_Click(sender, e);
            _config.ZapiszKonfiguracje();
        }

        private void btn_clientConf_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("client");
            if (keyExists)
            {
                ChangeConfigPage(userControls["client"]);
            }
            else
            {
                userControls.Add("client", new ConfigClientBoard(_config));
                ChangeConfigPage(userControls["client"]);
            }
            SetButtons(btn_clientConf);
        }

        private void btn_taskConf_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("task");
            if (keyExists)
            {
                ChangeConfigPage(userControls["task"]);
            }
            else
            {
                userControls.Add("task", new ConfigClientBoard(_config));
                ChangeConfigPage(userControls["task"]);
            }
            SetButtons(btn_clientConf);
        }

        private void SetButtons(Button activeButton)
        {
            btn_queueConf.IsEnabled = true;
            btn_clientConf.IsEnabled = true;
            btn_connectionConf.IsEnabled = true;
            activeButton.IsEnabled = false;
            
        }
    }
}

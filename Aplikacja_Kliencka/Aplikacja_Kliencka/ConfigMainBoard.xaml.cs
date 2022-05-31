using FontAwesome.Sharp;
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
        Button curButton;
        MainWindow _mainWindow;
        string _mainWindowLabel;
        public ConfigMainBoard(ref ConfigClient config, MainWindow mainWindow)
        {
            _config = config;
            InitializeComponent();
            userControls.Add("conection", new ConfigConectionBoard(_config));
            userControls.Add("queue", new ConfigQueueBoard(_config));
            userControls.Add("client", new ConfigClientBoard(_config));
            userControls.Add("task", new ConfigTaskBoard(_config));
            _mainWindow = mainWindow;
            _mainWindowLabel = mainWindow.lbl_heder_label.Content.ToString();
        }

        public void btn_connectionConf_Click(object sender=null, RoutedEventArgs e=null)
        {
            bool keyExists = userControls.ContainsKey("conection");
            ActivateButton(sender, Color.FromRgb(0, 0, 0));
            if (keyExists)
            {
                ChangeConfigPage(userControls["conection"]);
            }
            else
            {
                userControls.Add("conection", new ConfigConectionBoard(_config));
                ChangeConfigPage(userControls["conection"]);
            }
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

        public void btn_queueConf_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("queue");
            ActivateButton(sender, Color.FromRgb(0, 0, 0));
            if (keyExists)
            {
                ChangeConfigPage(userControls["queue"]);
            }
            else
            {
                userControls.Add("queue", new ConfigQueueBoard(_config));
                ChangeConfigPage(userControls["queue"]);
            }
        }

        private void btn_saveToFile_Click(object sender, RoutedEventArgs e)
        {
            this.btn_save_Click(sender, e);
            _config.ZapiszKonfiguracje();
        }

        public void btn_clientConf_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("client");
            ActivateButton(sender, Color.FromRgb(0, 0, 0));
            if (keyExists)
            {
                ChangeConfigPage(userControls["client"]);
            }
            else
            {
                userControls.Add("client", new ConfigClientBoard(_config));
                ChangeConfigPage(userControls["client"]);
            }
        }

        public void btn_taskConf_Click(object sender, RoutedEventArgs e)
        {
            bool keyExists = userControls.ContainsKey("task");
            ActivateButton(sender, Color.FromRgb(0,0,0));
            if (keyExists)
            {
                ChangeConfigPage(userControls["task"]);
            }
            else
            {
                userControls.Add("task", new ConfigClientBoard(_config));
                ChangeConfigPage(userControls["task"]);
            }
        }

        private void ActivateButton(object sender, Color color)
        {
            if (sender != null)
            {
                DeactivateButton();
                curButton = (Button)sender;
                curButton.Foreground = new SolidColorBrush(color);
                curButton.BorderThickness = new Thickness(0, 0, 0, 5);
                curButton.BorderBrush = new SolidColorBrush(color);
                ChangeHeader(sender);
            }
        }

        private void DeactivateButton()
        {
            if (curButton != null)
            {
                curButton.Foreground = Brushes.White;
                curButton.BorderThickness = new Thickness(0, 0, 0, 0);
                curButton.BorderBrush = Brushes.White;
            }
        }

        private void ChangeHeader(Object sender)
        {
            if (sender != null)
            {
                var x = (Button)sender;
                var s = (StackPanel)x.Content;
                var ico = (IconBlock)s.Children[0];
                var txt = (TextBlock)s.Children[1];

                _mainWindow.ico_heder_ico.Icon = ico.Icon;
                _mainWindow.lbl_heder_label.Content = _mainWindowLabel + "---->" +txt.Text;
            }
        }
    }
}

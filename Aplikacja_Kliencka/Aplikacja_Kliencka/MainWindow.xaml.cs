using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Pole przechowujące konfiguracje
        /// </summary>
        ConfigClient conf = new ConfigClient();
        Button curButton = null;
        Dashboard dashboard;
        WelcomeBoard welcomeBoard;
        ConfigMainBoard configMainBoard =null;
        bool configMainBoardClicked = false;
        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();

            dashboard = new Dashboard(conf);
            welcomeBoard = new WelcomeBoard();
            ChangeBoardView(welcomeBoard);
        }

        /// <summary>
        /// Metoda odpowiedzialna za załadowanie konfiguracji domyślnej lub z pliku
        /// </summary>
        private void LoadConfig()
        {
            string file = "config.cfg";
            if (File.Exists(file))
            {
                if (!conf.ZaladujDane())
                {
                    MessageBox.Show("Błąd w ładowaniu konfiguracji\nŁadowanie domyślnej");
                    conf.ZaladujDaneTestowe();
                }
            }
            else
            {
                MessageBox.Show("Nie znaleziono pliku konfiguracyjnego\n Ładowanie domyślnej konfiguracji");
                conf.ZaladujDaneTestowe();
                conf.ZapiszKonfiguracje();
            }
        }

        private void temp_Click(object sender, RoutedEventArgs e)
        {
            RabbitClient rabbit = new RabbitClient(conf);
            rabbit.ClientStart();
            Thread.Sleep(3000);
            rabbit.ClientStop();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            RabbitClient rabbit = new RabbitClient(conf);
            ClientView client = new ClientView(rabbit);
            //grid_test.Children.Add(client);
        }

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            lbl_heder_label.Content = "";
            ico_heder_ico.Icon = IconChar.Home;
            showConfigButtons(false);
            ChangeBoardView(welcomeBoard);
        }

        private void ChangeBoardView(UserControl userControl)
        {
            grid_board.Children.Clear();
            grid_board.Children.Add(userControl);
        }

        private void btn_dashBoard_Click(object sender, RoutedEventArgs e)
        {
            ActivateButton(sender, Color.FromRgb(255,255,255));
            showConfigButtons(false);
            ChangeBoardView(dashboard);
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            ActivateButton(sender, Color.FromRgb(255, 255, 255));
            showConfigButtons();
            if (configMainBoard == null)
            {
                configMainBoard = new ConfigMainBoard(ref conf, this);
            }
            ChangeBoardView(configMainBoard);
        }
        private void ActivateButton(object sender, Color color)
        {
            if (sender != null)
            {
                DeactivateButton();
                curButton = (Button)sender;
                curButton.Foreground = new SolidColorBrush(color);
                curButton.BorderThickness = new Thickness(10, 0, 0, 0);
                curButton.BorderBrush = new SolidColorBrush(color);
                ChangeHeader(sender);
            }
        }

        private void DeactivateButton()
        {
            if (curButton != null)
            {
                curButton.Foreground = Brushes.Chocolate;
                curButton.BorderThickness = new Thickness(0, 0, 0, 0);
                curButton.BorderBrush = Brushes.Chocolate;
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

                ico_heder_ico.Icon = ico.Icon;
                lbl_heder_label.Content = txt.Text;
            }
        }

        private void showConfigButtons(bool fromConfigButton=true)
        {
            if (!configMainBoardClicked && fromConfigButton)
            {
                btn_settings_con.Visibility = Visibility.Visible;
                btn_settings_que.Visibility = Visibility.Visible;
                btn_settings_cli.Visibility = Visibility.Visible;
                btn_settings_task.Visibility = Visibility.Visible;
            }
            else
            {
                btn_settings_con.Visibility = Visibility.Collapsed;
                btn_settings_que.Visibility = Visibility.Collapsed;
                btn_settings_cli.Visibility = Visibility.Collapsed;
                btn_settings_task.Visibility = Visibility.Collapsed;
            }
            if(configMainBoardClicked || fromConfigButton)
            {
                configMainBoardClicked = !configMainBoardClicked;
            }
        }

        private void btn_settings_con_Click(object sender, RoutedEventArgs e)
        {
            if (configMainBoard == null)
            {
                configMainBoard = new ConfigMainBoard(ref conf, this);
            }
            configMainBoard.btn_clientConf_Click(sender, e);
            ChangeBoardView(configMainBoard);
        }

        private void btn_settings_que_Click(object sender, RoutedEventArgs e)
        {
            if (configMainBoard == null)
            {
                configMainBoard = new ConfigMainBoard(ref conf, this);
            }
            configMainBoard.btn_queueConf_Click(sender, e);
            ChangeBoardView(configMainBoard);
        }

        private void btn_settings_cli_Click(object sender, RoutedEventArgs e)
        {
            if (configMainBoard == null)
            {
                configMainBoard = new ConfigMainBoard(ref conf, this);
            }
            configMainBoard.btn_clientConf_Click(sender, e);
            ChangeBoardView(configMainBoard);
        }

        private void btn_settings_task_Click(object sender, RoutedEventArgs e)
        {
            if (configMainBoard == null)
            {
                configMainBoard = new ConfigMainBoard(ref conf, this);
            }
            configMainBoard.btn_taskConf_Click(sender, e);
            ChangeBoardView(configMainBoard);
        }
    }
}

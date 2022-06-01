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
        ConfigClient conf = new ConfigClient();
        Button curButton = null;
        Button curViewButton = null;
        Dashboard dashboard;
        WelcomeBoard welcomeBoard;
        ConfigMainBoard configMainBoard =null;
        List<ClientView> clientViews = new List<ClientView>();
        List<Button> buttonsOfClientsViews = new List<Button>();
        bool configMainBoardClicked = false;
        bool viewButtonClicked = false;
        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();

            dashboard = new Dashboard(conf, this);
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
            ShowViewButton(false);
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
            ShowViewButton(false);
            ChangeBoardView(dashboard);
        }

        private void btn_settings_Click(object sender, RoutedEventArgs e)
        {
            ActivateButton(sender, Color.FromRgb(255, 255, 255));
            showConfigButtons();
            ShowViewButton(false);
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

        public void add_new_view(ClientView clientView)
        {
            clientViews.Add(clientView);
        }

        public List<ClientView> get_all_views()
        {
            return this.clientViews;
        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            ActivateButton(sender, Color.FromRgb(255, 255, 255));
            showConfigButtons(false);
            grid_views.Height = clientViews.Count * 60;
            for (int i = buttonsOfClientsViews.Count; i < clientViews.Count; i++)
            {
                var button = CreateButton(i, clientViews[i].getClient().getName());
                grid_views.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(button, i);
                buttonsOfClientsViews.Add(button);
                grid_views.Children.Add(button);
            }
            ShowViewButton();
        }

        private Button CreateButton(int i, string name)
        {
            Button button = new Button();
            StackPanel stackPanel = new StackPanel();
            IconBlock iconBlock = new IconBlock();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = name;
            textBlock.FontSize = 36;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.Margin = new Thickness(5, 0, 0, 0);
            iconBlock.Icon = IconChar.UserAstronaut;
            iconBlock.VerticalAlignment = VerticalAlignment.Center;
            iconBlock.HorizontalAlignment = HorizontalAlignment.Left;
            iconBlock.Margin = new Thickness(5, 0, 0, 0);
            iconBlock.FontSize = 36;
            stackPanel.Width = 290;
            stackPanel.Height = 60;
            stackPanel.Margin = new Thickness(0, 0, 0, 0);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.VerticalAlignment = VerticalAlignment.Center;
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(iconBlock);
            stackPanel.Children.Add(textBlock);
            button.Content = stackPanel;
            button.Height = 60;
            button.Width = 290;
            button.Margin = new Thickness(10, 0, 0, 0);
            button.Foreground = Brushes.Chocolate;
            button.Background = Brushes.Transparent;
            button.BorderBrush = Brushes.Transparent;
            button.Name = "btn_clientView_"+i.ToString();
            button.Click += ChangeViewButton;
            button.VerticalAlignment = VerticalAlignment.Top;
            return button;
        }

        private void ChangeViewButton(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            string clientId = button.Name;
            clientId = clientId.Substring(clientId.IndexOf("View_")+5);
            int id;
            try
            {
                id = int.Parse(clientId);
                if( id< clientViews.Count)
                {
                    var temp1 = new ClientWindow(clientViews[id].getClient());
                    ChangeBoardView(temp1);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ActivateViewButton(sender, Color.FromRgb(0, 255, 0));
        }

        private void ShowViewButton(bool fromViewButton=true, bool valueChanged=false)
        {
            if (!fromViewButton)
            {
                grid_views.Visibility = Visibility.Collapsed;
                viewButtonClicked = false;
                DeactivateViewButton();
                MoveSettingsButton(false);
                curViewButton = null;
            }
            else
            {
                if (!viewButtonClicked)
                {
                    grid_views.Visibility = Visibility.Visible;
                    MoveSettingsButton(true);
                }
                else
                {
                    grid_views.Visibility = Visibility.Collapsed;
                    DeactivateViewButton();
                    MoveSettingsButton(false);
                    curViewButton = null;
                }
                viewButtonClicked = !viewButtonClicked;
            }
        }

        private void ActivateViewButton(object sender, Color color)
        {
            if (sender != null)
            {
                DeactivateViewButton();
                curViewButton = (Button)sender;
                curViewButton.Foreground = new SolidColorBrush(color);
                curViewButton.BorderThickness = new Thickness(10, 0, 0, 0);
                curViewButton.BorderBrush = new SolidColorBrush(color);
                ChangeHeader(sender);
            }
        }

        private void DeactivateViewButton()
        {
            if (curViewButton != null)
            {
                curViewButton.Foreground = Brushes.Chocolate;
                curViewButton.BorderThickness = new Thickness(0, 0, 0, 0);
                curViewButton.BorderBrush = Brushes.Chocolate;
            }
        }

        private void MoveSettingsButton(bool viewOpen)
        {
            int i = 140;
            int m = 70;
            if (viewOpen)
            {
                i = 140 + clientViews.Count * 60;
            }
            btn_settings.Margin = new Thickness(0, i, 0, 0);
            btn_settings_con.Margin = new Thickness(0, i + 1 * m, 0, 0);
            btn_settings_que.Margin = new Thickness(0, i + 2 * m, 0, 0);
            btn_settings_cli.Margin = new Thickness(0, i + 3 * m, 0, 0);
            btn_settings_task.Margin = new Thickness(0, i + 4 * m, 0, 0);
        }
    }
}

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
    /// Logika interakcji dla klasy Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        ConfigClient _config;
        List<ClientView> clientViews=new List<ClientView>();
        int maxNumOfClients = 0;
        int currentNumOfClients = 0;
        MainWindow _mainWindow;
        public Dashboard(ConfigClient config, MainWindow mainWindow)
        {
            _config = config;
            _mainWindow = mainWindow;
            InitializeComponent();

            //Wpisanie informacji
            maxNumOfClients = _config.ClientCount;
            currentNumOfClients = clientViews.Count;
            UpdateLabels();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (maxNumOfClients > currentNumOfClients)
            {
                RabbitClient rabbitClient = new RabbitClient(_config, _config.ClientsNames[currentNumOfClients]);
                ClientView tempView = new ClientView(rabbitClient);
                _mainWindow.add_new_view(tempView);
                add_new_view(tempView);
                currentNumOfClients++;
            }

            if (maxNumOfClients<= currentNumOfClients)
            {
                btn_add.IsEnabled = false;
            }
            UpdateLabels();
        }

        private void add_new_view(ClientView userControl)
        {
            grid_activeClientsView.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(userControl, currentNumOfClients);
            clientViews.Add(userControl);
            grid_activeClientsView.Children.Add(clientViews[currentNumOfClients]);
        }

        private void UpdateLabels()
        {
            lbl_numberOfActiveClients.Content = "Liczba aktywnych klientow: " + currentNumOfClients;
            lbl_maxNumberOfActiveClients.Content = "Maksymalna liczba aktywnych klientow: " + maxNumOfClients;
        }

        private void btn_add_x_Click(object sender, RoutedEventArgs e)
        {
            int counter = clientViews.Count;
            int i = 0;
            int temp = counter;


            if (!int.TryParse(txt_add_x.Text, out i))
            {
                MessageBox.Show("Błędna liczba klientów do dodania");
                throw new Exception();
            }

            for (int j = 0; j < i; j++)
            {
                if (counter < _config.ClientCount)
                {
                    btn_add_Click(sender, e);
                }
            }

            if (temp + i > _config.ClientCount)
            {
                temp = _config.ClientCount - temp;
                MessageBox.Show("Osiągnięto limit klientów \nDodano " + temp + " Klientów");
            }
        }

        private void txt_add_x_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i = 0;
            if(btn_add_x != null)
            {
                if (int.TryParse(txt_add_x.Text, out i))
                {
                    btn_add_x.Content = ("Dodaj: " + i);
                    btn_add_x.IsEnabled = true;
                }
                else 
                {
                    btn_add_x.IsEnabled = false;
                }
            }
        }

        private void btn_start_all_Click(object sender, RoutedEventArgs e)
        {
            foreach(var c in clientViews)
            {
                c.Start();
            }
        }

        private void btn_stop_all_Click(object sender, RoutedEventArgs e)
        {
            foreach (var c in clientViews)
            {
                c.Stop();
            }
        }
    }
}

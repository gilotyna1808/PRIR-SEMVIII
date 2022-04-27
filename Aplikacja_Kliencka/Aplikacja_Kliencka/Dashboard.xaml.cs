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
        public Dashboard(ConfigClient config)
        {
            _config = config;
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
    }
}

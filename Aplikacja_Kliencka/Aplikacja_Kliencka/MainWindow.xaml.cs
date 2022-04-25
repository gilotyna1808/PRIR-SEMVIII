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

        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
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
            rabbit.KlientStart();
            Thread.Sleep(3000);
            rabbit.KlientStop();
        }

        private void btn_config_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow(ref conf);
            configWindow.ShowDialog();
        }
    }
}

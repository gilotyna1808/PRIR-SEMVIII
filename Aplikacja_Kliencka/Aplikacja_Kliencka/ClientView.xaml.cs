using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy ClientView.xaml
    /// </summary>
    public partial class ClientView : UserControl
    {
        /// <summary>
        /// Pole przechowujące klienta
        /// </summary>
        RabbitClient _client;

        bool buttonStart =true;
        bool buttonStop =false;
        bool flagStop = false;

        public ClientView(RabbitClient rabbitClient)
        {
            InitializeComponent();

            //Inicjalizacja
            InitializeComponent();

            //Wpisanie argumentów do pól
            this._client = rabbitClient;

            //Wypisanie infromacji w etykietach
            var clientName = "Klient: " + _client.getName();
            lbl_clientName.Content = clientName;
        }

        /// <summary>
        /// Uruchom klienta
        /// </summary>
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            _client.ClientStart();
            buttonStart = false;
            buttonStop = true;
            flagStop = false;
            KonfiguracjaPrzyciskow();
            /*if (backgroudWorkEnd)
            {
                watekAktualizacjaStatusu = new Thread(() => aktualizujStatus(2000));
                watekAktualizacjaStatusu.Start();
            }*/
        }

        /// <summary>
        /// Otwórz Log
        /// </summary>
        private void btn_log_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", (_client.getName() + ".txt"));
        }

        /// <summary>
        /// Zatrzymaj klienta
        /// </summary>
        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() => _client.ClientStop()).Start();
        }

        /// <summary>
        /// Metoda umożliwiająca zmiane stanów przycisków asynchronicznie
        /// </summary>
        private void KonfiguracjaPrzyciskow()
        {
            bool uiAccessBtnStart = btn_start.Dispatcher.CheckAccess();
            bool uiAccessBtnStop = btn_stop.Dispatcher.CheckAccess();
            if (uiAccessBtnStart) btn_start.IsEnabled = buttonStart;
            else btn_start.Dispatcher.Invoke(() => btn_start.IsEnabled = buttonStart);
            if (uiAccessBtnStop) btn_stop.IsEnabled = buttonStop;
            else btn_stop.Dispatcher.Invoke(() => btn_stop.IsEnabled = buttonStop);
        }
    }
}

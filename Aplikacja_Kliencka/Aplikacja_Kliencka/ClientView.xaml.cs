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

        /// <summary>
        /// Pole przechowujące informacje o dostępności przycisku start
        /// </summary>
        private bool buttonStart = true;

        /// <summary>
        /// Pole przechowujące informacje o dostępności przycisku stop
        /// </summary>
        private bool buttonStop = false;

        /// <summary>
        /// Pole przechowujące informacje o zakonczeniu pracy wątka w tle
        /// </summary>
        private bool backgroudWorkEnd = true;

        /// <summary>
        /// Pole przechowujące informacje o zakończeniu pracy
        /// </summary>
        private bool flagStop = false;

        /// <summary>
        /// Pole przechowujące wątek działający w tle
        /// </summary>
        private Thread watekAktualizacjaStatusu = null;

        public ClientView(RabbitClient rabbitClient)
        {
            //Inicjalizacja
            InitializeComponent();

            //Wpisanie argumentów do pól
            this._client = rabbitClient;

            //Wypisanie infromacji w etykietach
            var clientName = "Klient: " + _client.getName();
            lbl_clientName.Content = clientName;
            lbl_status.Content = ConfigClient.getStan(_client.getStan());
        }

        /// <summary>
        /// Pobierz instancje klienta
        /// </summary>
        public RabbitClient getClient()
        {
            return this._client;
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
            if (backgroudWorkEnd)
            {
                watekAktualizacjaStatusu = new Thread(() => aktualizujStatus(2000));
                watekAktualizacjaStatusu.Start();
            }
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

        /// <summary>
        /// Metoda wypisujaca informacje asynchronicznie
        /// </summary>
        private void aktualizujStatus(int time)
        {
            try
            {
                //Działaj dopóki flaga stop nie zostanie podniesiona
                while (!flagStop)
                {
                    //Sprawdzenie dostępności do etykiety
                    bool uiAccess = lbl_status.Dispatcher.CheckAccess();
                    if (uiAccess) lbl_status.Content = ConfigClient.getStan(_client.getStan());
                    else lbl_status.Dispatcher.Invoke(() => { lbl_status.Content = ConfigClient.getStan(_client.getStan()); });

                    //Uspanie wątka na zadany czas
                    Thread.Sleep(time);

                    //Wykonaj jeżeli klient został zatrzymany
                    if (_client.getStan() == StanZadania.Koniec)
                    {
                        buttonStart = true;
                        buttonStop = false;
                        flagStop = true;
                        KonfiguracjaPrzyciskow();
                        if (uiAccess) lbl_status.Content = ConfigClient.getStan(_client.getStan());
                        else lbl_status.Dispatcher.Invoke(() => { lbl_status.Content = ConfigClient.getStan(_client.getStan()); });
                    }
                }

            }
            catch (ThreadInterruptedException ex)
            {
                Debug.WriteLine("Przewanie watku klientPodglad");
            }
            catch (Exception ex)
            {

            }
            backgroudWorkEnd = true;
        }

        public void ForceStop()
        {
            flagStop = true;
            _client.ForceStop();
            if (watekAktualizacjaStatusu != null) watekAktualizacjaStatusu.Interrupt();
        }

        public void Start()
        {
            btn_start_Click(null, null);
        }

        public void Stop()
        {
            btn_stop_Click(null, null);
        }
    }
}

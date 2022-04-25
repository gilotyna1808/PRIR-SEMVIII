using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_Kliencka
{
    public class ConfigClient
    {
        /// <summary>
        /// Pole przechowujące nazwę użytkownika RabitMQ
        /// </summary
        private string _rabitMQ_UserName;

        /// <summary>
        /// Pole przechowujące hasło użytkownika RabitMQ
        /// </summary
        private string _rabitMQ_Password;

        /// <summary>
        /// Pole przechowujące nazwę virtualHosta RabitMQ
        /// </summary
        private string _rabitMQ_VirualHost;

        /// <summary>
        /// Pole przechowujące nazwę hosta RabitMQ
        /// </summary
        private string _rabitMQ_HostName;

        /// <summary>
        /// Pole przechowujące port hosta RabitMQ
        /// </summary
        private int _rabitMQ_Port = 0;

        /// <summary>
        /// Pole przechowujące nazwę kolejki z zadaniami RabitMQ
        /// </summary
        private string _rabitMQ_QueueRecive;

        /// <summary>
        /// Flaga poprawności odczytancyh danych
        /// </summary
        private bool _flagPoprawne;

        public string RabitMQ_UserName { get => _rabitMQ_UserName; set => _rabitMQ_UserName = value; }
        public string RabitMQ_Password { get => _rabitMQ_Password; set => _rabitMQ_Password = value; }
        public string RabitMQ_VirualHost { get => _rabitMQ_VirualHost; set => _rabitMQ_VirualHost = value; }
        public string RabitMQ_HostName { get => _rabitMQ_HostName; set => _rabitMQ_HostName = value; }
        public string RabitMQ_QueueRecive { get => _rabitMQ_QueueRecive; set => _rabitMQ_QueueRecive = value; }
        public int RabitMQ_Port { get => _rabitMQ_Port; set => _rabitMQ_Port = value; }

        public void ZaladujDaneTestowe()
        {
            _rabitMQ_UserName = "guest";
            _rabitMQ_Password = "guest";
            _rabitMQ_VirualHost = "";
            _rabitMQ_HostName = @"localhost";
            _rabitMQ_Port = 5672;
            _rabitMQ_QueueRecive = "test1";
        }

        /// <summary>
        /// Metoda zapisująca konfiguracje do pliku
        /// </summary
        public void ZapiszKonfiguracje()
        {
            using (StreamWriter sw = new StreamWriter("config.cfg"))
            {
                sw.WriteLine("RabitMQ UserName= " + _rabitMQ_UserName);
                sw.WriteLine("RabitMQ Password= " + _rabitMQ_Password);
                sw.WriteLine("RabitMQ VHostName= " + _rabitMQ_VirualHost);
                sw.WriteLine("RabitMQ HostName= " + _rabitMQ_HostName);
                sw.WriteLine("RabitMQ Port= " + _rabitMQ_Port);
                sw.WriteLine("Nazwa kolejki z zadaniami= " + _rabitMQ_QueueRecive);
            }
        }

        /// <summary>
        /// Metoda odczytująca konfiguracje z pliku
        /// </summary
        public bool ZaladujDane()
        {
            using (StreamReader sr = new StreamReader("config.cfg"))
            {
                //Zmienne pomocnicze
                _flagPoprawne = false;
                string rabitMqUserName = null;
                string rabitMqPassword = null;
                string rabitMqVHostName = null;
                string rabitMqHostName = null;
                string rabitMqPort = null;
                string rabitMqQueueR = null;
                //Odczytywanie pliku
                if ((rabitMqUserName = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqPassword = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqVHostName = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqHostName = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqPort = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqQueueR = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                
                //Zmiana wartosci odczytanych na użyteczne dane
                try
                {
                    _rabitMQ_UserName = rabitMqUserName.Substring(rabitMqUserName.IndexOf("=") + 2);
                    _rabitMQ_Password = rabitMqPassword.Substring(rabitMqPassword.IndexOf("=") + 2);
                    _rabitMQ_VirualHost = rabitMqVHostName.Substring(rabitMqVHostName.IndexOf("=") + 2);
                    _rabitMQ_HostName = rabitMqHostName.Substring(rabitMqHostName.IndexOf("=") + 2);
                    _rabitMQ_Port = int.Parse(rabitMqPort.Substring(rabitMqPort.IndexOf("=") + 2));
                    _rabitMQ_QueueRecive = rabitMqQueueR.Substring(rabitMqQueueR.IndexOf("=") + 2);
                }
                catch (Exception ex)
                {
                    _flagPoprawne = false;
                    return false;
                }

                //Ustaienie flag na powodzenie
                _flagPoprawne = true;
                return true;
            }
        }

    }
}

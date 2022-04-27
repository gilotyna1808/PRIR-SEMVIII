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
        private List<string> _rabitMQ_QueueRecive;

        /// <summary>
        /// Pole przechowujące ilosc kolejek z zadaniami RabitMQ
        /// </summary
        private int _rabitMQ_QueueCount = 0;

        /// <summary>
        /// Pole przechowujące nazwę kolejki z zadaniami RabitMQ
        /// </summary
        private List<string> _clientsNames;

        /// <summary>
        /// Pole przechowujące ilosc kolejek z zadaniami RabitMQ
        /// </summary
        private int _clientsCount = 0;

        /// <summary>
        /// Flaga poprawności odczytancyh danych
        /// </summary
        private bool _flagPoprawne;


        public string RabitMQ_UserName { get => _rabitMQ_UserName; set => _rabitMQ_UserName = value; }
        public string RabitMQ_Password { get => _rabitMQ_Password; set => _rabitMQ_Password = value; }
        public string RabitMQ_VirualHost { get => _rabitMQ_VirualHost; set => _rabitMQ_VirualHost = value; }
        public string RabitMQ_HostName { get => _rabitMQ_HostName; set => _rabitMQ_HostName = value; }
        public List<string> RabitMQ_QueueRecive { get => _rabitMQ_QueueRecive; set => _rabitMQ_QueueRecive = value; }
        public int RabitMQ_Port { get => _rabitMQ_Port; set => _rabitMQ_Port = value; }
        public int RabitMQ_QueueCount { get => _rabitMQ_QueueCount; set => _rabitMQ_QueueCount = value; }
        public List<string> ClientsNames { get => _clientsNames; set => _clientsNames = value; }
        public int ClientCount { get => _clientsCount; set => _clientsCount = value; }

        public void ZaladujDaneTestowe()
        {
            _rabitMQ_UserName = "guest";
            _rabitMQ_Password = "guest";
            _rabitMQ_VirualHost = "";
            _rabitMQ_HostName = @"localhost";
            _rabitMQ_Port = 5672;
            _rabitMQ_QueueCount = 1;
            _rabitMQ_QueueRecive = new List<string>() { "test"};
            _clientsCount = 2;
            _clientsNames = new List<string> { "klient1", "klient2" };
        }

        /// <summary>
        /// Metoda zapisująca konfiguracje do pliku
        /// </summary
        public void ZapiszKonfiguracje()
        {
            using (StreamWriter sw = new StreamWriter("config.cfg"))
            {
                sw.WriteLine("RabbitMQ UserName= " + _rabitMQ_UserName);
                sw.WriteLine("RabbitMQ Password= " + _rabitMQ_Password);
                sw.WriteLine("RabbitMQ VHostName= " + _rabitMQ_VirualHost);
                sw.WriteLine("RabbitMQ HostName= " + _rabitMQ_HostName);
                sw.WriteLine("RabbitMQ Port= " + _rabitMQ_Port);
                sw.WriteLine("RabbitMQ ilosc kolejek z zadaniami= " + _rabitMQ_QueueCount);
                for(int i = 0; i < _rabitMQ_QueueCount; i++)
                {
                    sw.WriteLine("Nazwa kolejki z zadaniami= " + _rabitMQ_QueueRecive[i]);
                }
                sw.WriteLine("Maksymalna ilosc jednoczesnych klientow= " + _clientsCount);
                for (int i = 0; i < _clientsCount; i++)
                {
                    sw.WriteLine("Nazwa klienta= " + _clientsNames[i]);
                }
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
                string rabitMqQueueCount = null;
                string clientCount = null;
                //Odczytywanie pliku
                if ((rabitMqUserName = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqPassword = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqVHostName = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqHostName = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                if ((rabitMqPort = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                try
                {
                    if ((rabitMqQueueCount = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                    RabitMQ_QueueCount = int.Parse(rabitMqQueueCount.Substring(rabitMqQueueCount.IndexOf("=") + 2));
                    List<string> tempQueueNames = new List<string>();
                    for(int i =0; i< RabitMQ_QueueCount; i++)
                    {
                        string s = sr.ReadLine();
                        tempQueueNames.Add(s.Substring(s.IndexOf("=") + 2));
                    }
                    RabitMQ_QueueRecive = tempQueueNames;
                    
                    if ((clientCount = sr.ReadLine()) == null) { _flagPoprawne = false; return false; }
                    ClientCount = int.Parse(clientCount.Substring(clientCount.IndexOf("=") + 2));
                    List<string> tempClientNames = new List<string>();
                    for (int i = 0; i < ClientCount; i++)
                    {
                        string s = sr.ReadLine();
                        tempClientNames.Add(s.Substring(s.IndexOf("=") + 2));
                    }
                    ClientsNames = tempClientNames;
                }
                catch (Exception ex)
                {
                    _flagPoprawne = false;
                    return false;
                }
                //Zmiana wartosci odczytanych na użyteczne dane
                try
                {
                    _rabitMQ_UserName = rabitMqUserName.Substring(rabitMqUserName.IndexOf("=") + 2);
                    _rabitMQ_Password = rabitMqPassword.Substring(rabitMqPassword.IndexOf("=") + 2);
                    _rabitMQ_VirualHost = rabitMqVHostName.Substring(rabitMqVHostName.IndexOf("=") + 2);
                    _rabitMQ_HostName = rabitMqHostName.Substring(rabitMqHostName.IndexOf("=") + 2);
                    _rabitMQ_Port = int.Parse(rabitMqPort.Substring(rabitMqPort.IndexOf("=") + 2));
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

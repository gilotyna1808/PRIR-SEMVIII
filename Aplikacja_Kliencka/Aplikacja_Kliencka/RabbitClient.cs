using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Aplikacja_Kliencka
{
    public class RabbitClient
    {
        /// <summary>
        /// Flaga działania klienta
        /// </summary>
        private bool flagIsRuning = false;
        /// <summary>
        /// Pole przehowujące wątek, który w tle jest połączony z serwisem
        /// </summary>
        private Thread thread = null;
        /// <summary>
        /// Semafor czekający na zakończenie pracy klienta
        /// </summary>
        protected volatile AutoResetEvent semaphore = new AutoResetEvent(true);
        /// <summary>
        /// Flaga zatrzymania pracy klienta
        /// </summary>
        private bool flagStop = true;
        /// <summary>
        /// Pole przechowujące konfiguracje
        /// </summary>
        ConfigClient _config = new ConfigClient();
        /// <summary>
        /// Pole przechowujące nazwę klienta
        /// </summary>
        private string _name;
        /// <summary>
        /// Pole przechowujące stan zadania
        /// </summary
        private StanZadania _status = StanZadania.NieUruchomione;
        /// <summary>
        /// Pole przechowujące obecne zadanie
        /// </summary
        private string _task = "";

        public RabbitClient(ConfigClient config, string name="")
        {
            this._config = config;
            _name = name;
        }

        /// <summary>
        /// Metoda uruchamiająca prace klienta
        /// </summary>
        public void ClientStart()
        {
            Debug.WriteLine("Klient:{0} Start",_name);
            if (!flagIsRuning)
            {
                thread = new Thread(() => CreateClient());
                _status = StanZadania.Czeka;
                thread.Start();
                string txtLog = "[" + DateTime.Now.ToString() + "] Uruchomienie klienta";
                WriteInFile(_name.ToString(), txtLog);
                flagStop = false;
            }
        }

        /// <summary>
        /// Uruchomienie procesu zatrzymywania klienta
        /// </summary>
        public void ClientStop()
        {
            flagStop = true;
            semaphore.Set();
        }

        /// <summary>
        /// Metoda tworzaca polaczenie miedzy klientam a serwisem
        /// </summary>
        private void CreateClient()
        {
            var factory = new ConnectionFactory() { 
                HostName = _config.RabitMQ_HostName,
                UserName = _config.RabitMQ_UserName,
                Password = _config.RabitMQ_Password,
                Port = _config.RabitMQ_Port,
            };
            try
            {
                flagIsRuning = true;
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //Ustawienie maksymalnej ilości przerabianych wiadomości
                        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: true);
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += Consumer_Received;
                        foreach (var queue in _config.RabitMQ_QueueRecive)
                        {
                            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
                        }
                        //channel.BasicConsume(queue: _config.RabitMQ_QueueRecive[0], autoAck: false, consumer: consumer);
                        while (!flagStop)
                        {
                            semaphore.WaitOne(-1, true);
                            if (flagStop) break;
                        }
                        flagIsRuning = false;
                        //Wpisanie informacji do logu
                        {
                            string txtLog = "[" + DateTime.Now.ToString() + "] Koniec polaczenia z serwisem kolejkowania";
                            WriteInFile(_name.ToString(), txtLog);
                        }
                        _status = StanZadania.Koniec;
                    }

                }
            }
            catch (ThreadInterruptedException ex)
            {
                Debug.WriteLine("Przewanie watku");
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                MessageBox.Show("Błąd połączenia\nHostname: " + factory.HostName + "\nUserName:" + factory.UserName + "\nPass:" + factory.Password + "\nVhost:" + factory.VirtualHost + "\n" + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Metoda konfigurująca dane połączeniowe
        /// </summary>
        private void KonfigurujPolaczenie(ref ConnectionFactory factory)
        {
            string userName = "guest";
            string password = "guest";
            string vHost = "";
            string hostName = @"localhost";
            int port = 5672;
            factory.UserName = userName;
            factory.Password = password;
            factory.HostName = hostName;
            factory.VirtualHost =  vHost;
            factory.Port = port;
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            //Powiadomienie, że zaczęto operacje na wiadomości
            Debug.WriteLine($"Klient: {_name} Pobranie wiadomosci");
            //Odbieranie wiadomości
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            message = message.Trim(new char[] { '"' });
            _task = message;
            //Wpisanie informacji do logu
            {
                string txtLog = "[" + DateTime.Now.ToString() + "] Pobranie wiadomości";
                WriteInFile(_name.ToString(), txtLog);
            }
            _status = StanZadania.Pracuje;
            //Podzielenie wiadomosci na parametry
            string[] param = message.Split(new char[] { ' ' });
            List<string> paramList = param.ToList();
            int exitCode = -1;
            StanZadania tempStan = StanZadania.ZakonczonoNiePowodzeniem;
            try
            {
                //Wpisanie informacji do logu
                {
                    string txtLog = "[" + DateTime.Now.ToString() + "] Uruchomienie Obliczen, Argumenty(" + message + ")";
                    WriteInFile(_name.ToString(), txtLog);
                }
                exitCode = DoWork(paramList);
                //Wpisanie informacji do logu
                {
                    string txtLog = "[" + DateTime.Now.ToString() + "] Koniec Obliczeń";
                    WriteInFile(_name.ToString(), txtLog);
                }
            }
            catch(Exception ex)
            {
                _status = StanZadania.Blad;
                MessageBox.Show(ex.Message);
            }
            if (exitCode != 0) _status = StanZadania.Pobrano;
            else _status = StanZadania.ZakonczonoPowodzeniem;
            //Potwierdzenie satusu zadania do serwera
            AddRes(param[0], _status, exitCode.ToString());
            
            //Potwierdzenie wykonania operacji do kolejki
            ((EventingBasicConsumer)sender).Model.BasicAck(e.DeliveryTag, false);

            _task = "";
            _status = StanZadania.Czeka;
            //Ustawienie stanu gotowości na kolejne zadanie
            semaphore.Set();
        }

        private int DoWork(List<String> param)
        {
            Debug.WriteLine(param[1]);
            int kodWyjsciowy = -1;
            if (param.Count < 1)
            {
                throw new Exception("Nie przekazano parametrow");
            }
            string task = param[1];
            param.RemoveAt(0);
            bool keyExists = _config.Tasks.ContainsKey(task);
            if (keyExists)
            {
                ProcessStartInfo process = new ProcessStartInfo(_config.Tasks.GetValueOrDefault(task));
                process.Arguments = CreteArgumentsLine(param);
                var proc = Process.Start(process);
                proc.WaitForExit();
                kodWyjsciowy = proc.ExitCode;
                //Wpisanie informacji do logu
                {
                    string txtLog = "[" + DateTime.Now.ToString() + "] Koniec Obliczeń, kod wyjsciowy programu" + kodWyjsciowy.ToString();
                    WriteInFile(_name, txtLog);
                }
            }
            else
            {
                throw new Exception(task + " Brak takiego zadania");
            }
            return kodWyjsciowy;
        }

        /// <summary>
        /// Metoda tworząca log
        /// </summary>
        private void WriteInFile(string file, string txt)
        {
            file += ".txt";
            //if (!File.Exists(file)) File.Create(file);
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(txt);
            }
        }

        /// <summary>
        /// Getter pobierający nazwe klienta
        /// </summary
        public string getName()
        {
            return this._name;
        }

        public string CreteArgumentsLine(List<string> param)
        {
            string res = "";
            for(int i = 1; i < param.Count; i++)
            {
                if (i != 1) res += " ";
                res += param[i];
            }
            return res;
        }

        /// <summary>
        /// Metoda łącząca się z kolejką i przekazująca informacje o statusie zadanai
        /// </summary
        private void AddRes(string msg, StanZadania stan, string blod = "")
        {
            var factory = new ConnectionFactory()
            {
                HostName = _config.RabitMQ_HostName,
                UserName = _config.RabitMQ_UserName,
                Password = _config.RabitMQ_Password,
                Port = _config.RabitMQ_Port,
            };

            try
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        if (blod != "" && blod!="0") stan = StanZadania.ZakonczonoNiePowodzeniem;
                        string message = msg + " " + ConfigClient.getStan(stan)+" " + blod;
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(
                            exchange: "",
                            routingKey: _config.RabitMQ_QueueSend,
                            basicProperties: null,
                            body: body
                            ); ;
                    }
                }
            }
            catch (RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                MessageBox.Show("Błąd połączenia\nHostname: " + factory.HostName + "\nUserName:" + factory.UserName + "\nPass:" + factory.Password + "\nVhost:" + _config.RabitMQ_VirualHost + "\n" + ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Getter pobierający wartość stanu zadania
        /// </summary
        public StanZadania getStan()
        {
            return this._status;
        }
        /// <summary>
        /// Getter pobierający wartość flagi informującej o uruchomieniu
        /// </summary
        public bool getStart()
        {
            return flagIsRuning;
        }
        /// <summary>
        /// Metoda wymuszenie zakończenia pracy wszystkich wątków
        /// </summary
        public void ForceStop()
        {
            flagStop = true;
            flagIsRuning = false;
            if (thread != null) thread.Interrupt();
        }
        /// <summary>
        /// Getter pobierający aktualnie wykonywane zadanie
        /// </summary
        public string getTask()
        {
            return _task;
        }
    }

}

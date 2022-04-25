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
    class RabbitClient
    {
        /// <summary>
        /// Flaga działania klienta
        /// </summary
        private bool flagIsRuning = false;
        /// <summary>
        /// Pole przehowujące wątek, który w tle jest połączony z serwisem
        /// </summary
        private Thread thread = null;
        /// <summary>
        /// Semafor czekający na zakończenie pracy klienta
        /// </summary
        protected volatile AutoResetEvent semaphore = new AutoResetEvent(true);
        /// <summary>
        /// Flaga zatrzymania pracy klienta
        /// </summary
        private bool flagStop = true;

        /// <summary>
        /// Metoda uruchamiająca prace klienta
        /// </summary
        public void KlientStart()
        {
            if (!flagIsRuning)
            {
                thread = new Thread(() => CreateClient());
                thread.Start();
            }
        }

        /// <summary>
        /// Uruchomienie procesu zatrzymywania klienta
        /// </summary
        public void KlientStop()
        {
            flagStop = true;
            semaphore.Set();
        }

        /// <summary>
        /// Metoda tworzaca polaczenie miedzy klientam a serwisem
        /// </summary
        private void CreateClient()
        {
            var factory = new ConnectionFactory() { HostName="localhost"};
            try
            {
                string queueName = "test1";
                flagIsRuning = true;
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        //Ustawienie maksymalnej ilości przerabianych wiadomości
                        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: true);
                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += Consumer_Received;
                        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                        while (!flagStop)
                        {
                            semaphore.WaitOne(-1, true);
                            if (flagStop) break;
                        }
                        flagIsRuning = false;
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
            semaphore.WaitOne(-1, true);

            //Odbieranie wiadomości
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            MessageBox.Show(message);

            //Ustawienie stanu gotowości na kolejne zadanie
            semaphore.Set();
        }
    }
}

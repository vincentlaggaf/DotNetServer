using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Contract;
using System.Threading;
using Job;
using System.ServiceModel.Channels;
using System.Xml;
using System.ServiceModel.Configuration;

namespace DotNETServer
{
    public class ServiceProvider
    {
        private Uri baseAddress;
        private ServiceHost host;
        public void ServiceStart()
        {
            //baseAddress = new Uri("net.tcp://localhost:8018/Job/MessageService");
            //NetTcpBinding binding = new NetTcpBinding();
            //binding.Security.Mode = SecurityMode.Message;

            //baseAddress = new Uri("http://localhost:9018/Job/MessageService");
            //WSHttpBinding binding = new WSHttpBinding();
           // binding.MaxReceivedMessageSize = 10485760;
           // XmlDictionaryReaderQuotas myReaderQuotas = new XmlDictionaryReaderQuotas();
           // myReaderQuotas.MaxStringContentLength = 10485760;
           // binding.GetType().GetProperty("ReaderQuotas").SetValue(binding, myReaderQuotas, null);
           // binding.ReceiveTimeout = TimeSpan.FromSeconds(20);

            host = new ServiceHost(typeof(Job.MessageService));
            try
            {
                host.Open();

                Console.WriteLine("Serveur started");
                Console.WriteLine("Type quit to stop the app");
                string command = "";
                while (command!="quit")
                {
                    command = Console.ReadLine();
                    if(command!="")
                    {
                        Console.WriteLine("Unknown command, type \"quit\" to stop the app");
                    }
                }
                host.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception rencontrée : {ex.Message}");
            }

        }
    }
}

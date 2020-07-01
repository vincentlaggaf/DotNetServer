using Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace clientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("net.tcp://localhost:8018/Job/MessageService");

            try
            {
                EndpointAddress endpointAddress = new EndpointAddress(baseAddress);
                NetTcpBinding binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.Message;
                var service = ChannelFactory<IMessageService>.CreateChannel(binding, endpointAddress);


                string login = "Xavier";
                string password = "Pass";
                string tokenApp = "tokenApp";
                string[] infos = { login, password };

                //Authentificate CASE 
                //Message message = new Message();
                //message.operationName = "returnResult";
                //message.info = "Demande de retour de résultat";

                //message.data = infos;
                //message.tokenApp = tokenApp;

                //Update Result CASE
                //Message message = new Message();
                //message.operationName = "updateResult";
                //message.tokenUser = "newToto";
                //message.tokenApp = "tokenApp";
                //string[] infosUpdate = { "Bonjour je suis un message décrypté !" };
                //message.data = infosUpdate;


                //Return Result CASE
                Message message = new Message();
                message.operationName = "updateResult";
                message.tokenUser = "newToto";
                message.tokenApp = "tokenApp";
                List<string> infoList = new List<string>();
                infoList.Add("Je suis le nom du fichier");
                infoList.Add("Je suis le contenu du fichier !!");
                message.data = infoList.ToArray();

                message = service.Servicing(message);

                Console.WriteLine($"information du message  : {message.info}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception rencontrée : {ex.Message}");

            }
        }


    }


}

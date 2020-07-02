using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contract;

namespace Job
{
    public class MessageService : IMessageService
    {
        private Authentificator authentificator;
        private Decipherer decipherer;
        private ResultReturner resultReturner;
        private string tokenApp = "tokenApp";
        private EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "WRITE_TO_SERVER_LOG");

        public Message Servicing(Message msg)
        {
            //Vérifier seulement le tokenApp pour l'authentification, puis vérifier le tokenUser pour les autres opérations ?
            if(CheckTokenApp(msg.tokenApp) && msg.tokenUser!=null)
            {

                //if(msg.tokenUser!=null)
                //{
                    switch (msg.operationName)
                    {
                        case "decipher":

                        if (IsNewOperationAllowed(msg.tokenUser))
                        {
                            ThreadPool.QueueUserWorkItem(decipherStarter => msg = StartDecipher(msg));
                            msg.info = "Demande de déchiffrement bien reçue";
                        }
                        else
                        {
                            msg.info = $"Error : only one decipher operation at a time is allowed and the user with the tokenUser : {msg.tokenUser} as already started a decipher operation.";
                        }
                        break;
                    case "returnResult":
                            //Console.WriteLine($"{msg.info}");
                            //msg.info = "Retour de la demande de résultat ";
                            msg = CheckResultReturner(msg);
                            break;
                    case "updateResult":
                            msg = UpdateResultReturner(msg,false);
                            break;
                    case "updateSecretResult":
                        msg = UpdateResultReturner(msg,true);
                            break;
                            //Peut être un case à rajouter pour gérer la réception des messages depuis la plateforme J2EE
                        default:
                            msg.info = "Invalid operation";
                            break;
                    }
                    if (msg.operationName == "authentificate")
                    {
                        msg.info += " : the user is already logged";
                        ThreadPool.QueueUserWorkItem(logWriter => AppendToLog($"{msg.info} as {msg.tokenUser} and is asking for an {msg.operationName} operation."));
                        Console.WriteLine($"{msg.info} as {msg.tokenUser} and is asking for an {msg.operationName} operation.");
                    }
                    else
                    {
                        ThreadPool.QueueUserWorkItem(logWriter => AppendToLog($"Request from the user using the userToken = {msg.tokenUser} : {msg.info} = {msg.operationName} "));
                        Console.WriteLine($"Request from the user using the userToken = {msg.tokenUser} : {msg.info} = {msg.operationName} ");

                    }
                //}
                //else
                //{
                //    msg.info = $"{msg.operationName} operation denied, tokenApp missing";

                //    ThreadPool.QueueUserWorkItem(logWriter => AppendToLog($"An operation was denied : {msg.operationName}. The tokenApp was missing"));
                //    Console.WriteLine($"An operation was denied : {msg.operationName}. The tokenApp was missing");

                //}

            }
            else if(CheckTokenApp(msg.tokenApp) && msg.tokenUser==null)
            { 
                if(msg.operationName== "authentificate")
                { 
                    msg.tokenUser = StartAuthenticate(msg);
                }
                else
                {

                }

            }
            else if(!CheckTokenApp(msg.tokenApp))
            {
                resultReturner = new ResultReturner();
                //ThreadPool.QueueUserWorkItem(mailSender=>resultReturner.SendMail("clé utilisée", "infos secrète", "NOM DE FICHIER","xavier.gistau@gmail.com"));
                ThreadPool.QueueUserWorkItem(logWriter=>AppendToLog($"Erreur : une application utilisant un token invalide a tenté d'accéder au serveur avec le tokenUser = {msg.tokenUser}."));
                Console.WriteLine($"Erreur : une application utilisant un token invalide a tenté d'accéder au serveur avec le tokenUser = {msg.tokenUser}. ");
                msg.info = "Token invalide : accès refusé";
            }
            

            return msg;
        }


        private string StartAuthenticate(Message msg)
        {
            //TODO : gérer les null pointeurs sur l'objet Data du message
            string login = msg.data[0].ToString();
            string password = msg.data[1].ToString();
            authentificator = new Authentificator();

            return authentificator.Authenticate(login, password) ;
        }

        private Message StartDecipher(Message msg)
        {
            if(decipherer==null)
            {
                decipherer = new Decipherer();
            }
            return decipherer.Decipher(msg);
        }

        private bool IsNewOperationAllowed(string tokenUser)
        {
            bool isNewOperationAllowed = false;
            if (resultReturner == null)
            {
                resultReturner = new ResultReturner();
            }

            if (resultReturner.CheckUserOperationStatus(tokenUser) == StatusOp.Waiting)
            {
                isNewOperationAllowed = true;
            }

            return isNewOperationAllowed;
        }

        private bool CheckTokenApp(string tokenApp)
        {
            bool tokenAppIsValid = tokenApp == this.tokenApp;

            return tokenAppIsValid;
        }

        private Message CheckResultReturner(Message msg)
        {
            if(resultReturner == null)
            {
                resultReturner = new ResultReturner();
            }

            return resultReturner.ReturnResult(msg);
        }

        private Message UpdateResultReturner(Message msg, bool isSecretInfoHere)
        {
            if (resultReturner == null)
            {
                resultReturner = new ResultReturner();
            }

            return resultReturner.UpdateResultReturner(msg,isSecretInfoHere);
        }

        private void AppendToLog(string textToAppend)
        {
            waitHandle.WaitOne();
            Console.WriteLine("Un thread commence à écrire");
            DateTime localDate = DateTime.Now;
            string text = $"{localDate.ToString(new System.Globalization.CultureInfo("fr-FR"))} : {textToAppend}";
            string path = "D:\\Users\\Utilisateur\\Documents\\log\\log.txt";
            if (!File.Exists(path))
            {
                //file doesn't exist
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(text);
                }

            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(text);
                }
            }
            waitHandle.Set();

        }
    }
}

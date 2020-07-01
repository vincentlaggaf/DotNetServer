using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Contract;

namespace Job
{
    class ResultReturner
    {
        
        public Message ReturnResult(Message msg)
        {

            StatusOp statusOp = CheckUserOperationStatus(msg.tokenUser);

            string updatedInfo = "";
            switch (statusOp)
            {

                case StatusOp.Waiting:
                    updatedInfo = $"Aucune opération de déchiffrement en cours pour l'utilisateur qui a pour tokenUser : {msg.tokenUser}";
                    msg = UpdateMessageInfos(msg, statusOp, updatedInfo);
                    break;
                case StatusOp.Working: 
                    updatedInfo = $"Une opération de déchiffrement est en cours pour l'utilisateur qui a pour tokenUser : {msg.tokenUser} mais n'est pas terminée";
                    msg = UpdateMessageInfos(msg, statusOp, updatedInfo);
                    break;
                case StatusOp.Finished:
                    updatedInfo = $"Une opération de déchiffrement a été effectuée pour l'utilisateur qui a pour tokenUser : {msg.tokenUser} et est en attente de finalisation. Envoi des fichiers en cours";
                    msg = UpdateMessageInfos(msg, statusOp, updatedInfo);
                    msg.data = ResultContainer.GetAwaitingResultForUser(msg.tokenUser);
                    break;

            }    

            return msg;
        }

        public StatusOp CheckUserOperationStatus(string tokenUser)
        {
            DAO dao = new DAO();
            StatusOp statusOp = dao.GetUserOperationStatus(tokenUser);

            return statusOp;
        }

        public Message UpdateResultReturnerData(Message msg)
        {
            ResultContainer.AddDecipherResultToWaitingList(msg.tokenUser, msg);
            StatusOp statusOp = StatusOp.Finished;
            string info = "Réception des fichiers déchiffrés terminée";
            Console.Write($"Les fichiers décryptés ont été reçu sur le serveur : {msg.data[0].ToString()}");
            DAO dao = new DAO();
            dao.UpdateRequestStatus(statusOp, msg.tokenUser);
            msg = UpdateMessageInfos(msg, statusOp, info);

            return msg;
        }
        //Réfléchir à comment faire la méthode qui sera appelée par Maxime depuis JEE
        public Message receivedInfoFromJEE(Message msg)
        {
            DAO dao = new DAO();
            string mail = dao.GetMailFromTokenUser(msg.tokenUser);
            string keyUsed = msg.data[0].ToString();
            string secretInfo = msg.data[1].ToString();
            string fileName = msg.data[2].ToString();
            string info = "Le mail de notification a été envoyé à l'utilisateur";
            SendMail(keyUsed, secretInfo, fileName, mail);
            msg = UpdateMessageInfos(msg,StatusOp.Sent,info);
            return msg; 
        }
        private Message UpdateMessageInfos(Message msg,StatusOp statusOp, string info)
        {
            msg.statusOp = statusOp;
            msg.info = info;

            return msg;
        }
        //PENSER A PASSER CETTE METHODE EN PRIVE UNE FOIS QUON RECEVRA LES MESSAGES DE J2EE
        public void SendMail(string keyUsed, string secretInfo,string fileName,string mail)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("decipherServerEMX@gmail.com");
            message.To.Add(new MailAddress(mail));
            message.Subject = "Results of the deciphering of your files ";
            message.IsBodyHtml = true;
            message.Body = BuildMailBody(keyUsed,secretInfo,fileName);
            smtpClient.Host = "smtp.gmail.com"; //for gmail host  
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("decipherServerEMX@gmail.com", "CesiXor64");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);

        }

        private string BuildMailBody(string keyUsed, string secretInfo, string fileName)
        {
            string body = "<font>Here is the information obtained after decrypting the files </font><br><br>";
            string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
            string htmlTableEnd = "</table>";
            string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
            string htmlHeaderRowEnd = "</tr>";
            string htmlTrStart = "<tr style=\"color:#555555;\">";
            string htmlTrEnd = "</tr>";
            string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
            string htmlTdEnd = "</td>";

            body += htmlTableStart;
            body += htmlHeaderRowStart;
            body += htmlTdStart + "File name" + htmlTdEnd;
            body += htmlTdStart + "Key used" + htmlTdEnd;
            body += htmlTdStart + "Secret info" + htmlTdEnd;
            body += htmlHeaderRowEnd;
            body = body + htmlTrStart;
            body = body + htmlTdStart + fileName + htmlTdEnd; 
            body = body + htmlTdStart + keyUsed + htmlTdEnd;  
            body = body + htmlTdStart + secretInfo + htmlTdEnd;
            body = body + htmlTrEnd;
            body = body + htmlTableEnd;

            return body;
        }
    }
}

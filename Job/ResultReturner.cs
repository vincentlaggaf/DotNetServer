using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Contract;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

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
                    DAO dao = new DAO();
                    dao.UpdateRequestStatus(StatusOp.Sent, msg.tokenUser);
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

        public Message UpdateResultReturner(Message msg,bool isInfoSecretHere)
        {
            StatusOp statusOp = StatusOp.Finished;
            string info = "Réception des fichiers déchiffrés terminée";
            ResultContainer.AddDecipherResultToWaitingList(msg.tokenUser, msg);
            Console.Write($"Les fichiers décryptés ont été reçu sur le serveur : {msg.data[0].ToString()}");
            DAO dao = new DAO();
            dao.UpdateRequestStatus(statusOp, msg.tokenUser);
            ReceivedInfoFromJEE(msg,isInfoSecretHere);
            msg = UpdateMessageInfos(msg, statusOp, info);

            return msg;
        }

        //Réfléchir à comment faire la méthode qui sera appelée par Maxime depuis JEE
        public Message ReceivedInfoFromJEE(Message msg, bool isInfoSecretHere)
        {
            DAO dao = new DAO();
            string mail = dao.GetMailFromTokenUser(msg.tokenUser);
            string fileName = msg.data[0].ToString();
            string confidenceRate = msg.data[3].ToString();
            string keyUsed = "";
            string secretInfo = "";
            if (isInfoSecretHere)
            {
                keyUsed = msg.data[1].ToString();
                secretInfo = msg.data[2].ToString();
            }
            else
                keyUsed = msg.data[2].ToString();

            string info = "Le mail de notification a été envoyé à l'utilisateur";
            SendMail(fileName, keyUsed, secretInfo, mail, isInfoSecretHere,confidenceRate);
            msg = UpdateMessageInfos(msg,StatusOp.Sent,info);

            return msg; 
        }

        private Message UpdateMessageInfos(Message msg,StatusOp statusOp, string info)
        {
            msg.statusOp = statusOp;
            msg.info = info;

            return msg;
        }
        //PENSER A PASSER CETTE METHODE EN PRIVE UNE FOIS QUON RECEVRA LES MESSAGES DE J2EE // PENSER A RAJOUTER LE TAUX DE CONFIANCE EN PARAMETRE et PEUT ETRE LE CONTENT DU FICHIER???
        public void SendMail(string fileName, string keyUsed, string secretInfo,string mail,bool isInfoSecretHere,string confidenceRate)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("decipherServerEMX@gmail.com");
            message.To.Add(new MailAddress(mail));
            message.Subject = $"Results of the deciphering of your files ";
            if (isInfoSecretHere)
                message.Subject +=": Secret information found !!";
            message.IsBodyHtml = true;
            message.Body = BuildMailBody(keyUsed,secretInfo,fileName, isInfoSecretHere);

            MemoryStream memoryStream = CreatePDFReport(fileName, confidenceRate, keyUsed, secretInfo, isInfoSecretHere);
            memoryStream.Position = 0;
            System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            Attachment attach = new Attachment(memoryStream, contentType);
            attach.ContentDisposition.FileName = $"Report.PDF";
            message.Attachments.Add(attach);


            smtpClient.Host = "smtp.gmail.com"; //for gmail host  
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("decipherServerEMX@gmail.com", "CesiXor64");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
            memoryStream.Close();

        }

        private string BuildMailBody(string keyUsed, string secretInfo, string fileName,bool isInfoSecretHere)
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
            if(isInfoSecretHere)
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

        private MemoryStream CreatePDFReport(string filename, string confidenceRate,string keyUsed,string secretInfo, bool isInfoSecretHere)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = $"Report of the confidence rate of the document \"{filename} \" ";

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Calibri", 11, XFontStyle.Regular);
            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(40, 100, page.Width-100, 220);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            tf.DrawString($"Confidence rate : {confidenceRate} Clé utilisée : {keyUsed} ", font, XBrushes.Black, rect, XStringFormats.TopLeft);
            if(isInfoSecretHere)
            {
                string secretInfoToWrite = $"The secret information was : {secretInfo}";
                rect = new XRect(20, 400, page.Width - 50, page.Height);
                gfx.DrawRectangle(XBrushes.SeaShell, rect);
                tf.Alignment = XParagraphAlignment.Justify;
                tf.DrawString(secretInfoToWrite, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            }


            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);

            return memoryStream;
        }
    }
}

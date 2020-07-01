using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contract;
using Job.ContractTypes;

namespace Job
{
    class Decipherer
    {
        private char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        private List<string> listkey = new List<string>();
        private List<string> textsToDecipher = new List<string>();
        private List<string> namesOfFilesToDecipher = new List<string>();
        

        public Message Decipher(Message msg)
        {
            DAO dao = new DAO();
            dao.CreateNewDecipherRequest(msg.tokenUser);

            msg.info = "Demande de déchiffrage reçue";

            for (int index = 0; index < msg.data.Length; index++)
            {
                if (index % 2 == 0)
                    namesOfFilesToDecipher.Add(msg.data[index].ToString());
                else
                    textsToDecipher.Add(msg.data[index].ToString());
            }


            CreateKey(0, 26);

            //var options = new ParallelOptions() { MaxDegreeOfParallelism = 4 };

            Parallel.ForEach(listkey, str =>
            {

                    
                    List<string> filesDeciphered = new List<string>();
                    List<string> textsDeciphered = new List<string>();
                    foreach (string strfile in textsToDecipher)
                    {
                        textsDeciphered.Add(Dechiffrer(str.ToCharArray(), strfile));
                    }

                    for (int index = 0; index < textsDeciphered.Count; index++)
                    {
                        filesDeciphered.Add(namesOfFilesToDecipher.ElementAt(index));
                        filesDeciphered.Add(textsDeciphered.ElementAt(index));

                    }
                    Console.WriteLine(filesDeciphered.ElementAt(1));
                    filesDeciphered.Add(str);

                    //FacadeEndpoint.FacadeEndpointClient facadeEndpointClient = new FacadeEndpoint.FacadeEndpointClient();
                    //FacadeEndpoint.soapMessage soapMessage = CreateSoapMessageFromMessage(msg, filesDeciphered);

                    //msg = CreateMessageFromSoapMessage(facadeEndpointClient.receiveDecipherOrder(soapMessage));
            });





            return msg;
        }

        private FacadeEndpoint.soapMessage CreateSoapMessageFromMessage(Message msg,List<string> filesDeciphered)
        {
            FacadeEndpoint.soapMessage soapMessage = new FacadeEndpoint.soapMessage
            {
                appVersion = msg.appVersion,
                data = filesDeciphered.ToArray(),
                info = msg.info,
                operationName = msg.operationName,
                operationVersion = msg.operationVersion,
                statutOp = (FacadeEndpoint.statutOp)(int)msg.statusOp,
                tokenApp = msg.tokenApp,
                tokenUser = msg.tokenUser
            };
            return soapMessage;
        }

        private Message CreateMessageFromSoapMessage(FacadeEndpoint.soapMessage soapMessage)
        {
            Message msg = new Message
            {
                appVersion = soapMessage.appVersion,
                data = soapMessage.data,
                info = soapMessage.info,
                operationName = soapMessage.operationName,
                operationVersion = soapMessage.operationVersion,
                statusOp = (StatusOp)(int)soapMessage.statutOp,
                tokenApp = soapMessage.tokenApp,
                tokenUser = soapMessage.tokenUser
            };

            return msg;
        }


        private void CreateKey(int start, int finish)
        {
            for (int i = start; i < finish; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        for (int l = 0; l < 26; l++)
                        {
                            listkey.Add(alphabet[i].ToString() + alphabet[j].ToString() + alphabet[k].ToString() + alphabet[l].ToString());
                        }
                    }
                }
            }
        }

        private string Dechiffrer(char[] key, string fileCiphered)
        {
            int indexKey = 0;

            string fileDEciphered = "";
            foreach (char c in fileCiphered)
            {
                if (indexKey == 4)
                    indexKey = 0;

                // récupérer l'ascii de la lettre de la clé 
                byte byteKey = (byte)key[indexKey];

                // récupérer l'ascii du char de la string
                byte byteChar = (byte)c;

                // comparer les deux et stocker dans un int
                int byteXOR = byteKey ^ byteChar;

                fileDEciphered += Convert.ToChar(byteXOR);
                indexKey++;
            }
            return fileDEciphered;
        }


    }
}

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

            ResultContainer.userAndNamesOfFilesToDecipherDic.Add(msg.tokenUser, new List<string>());
            ResultContainer.userAndContentOfFilesToDecipherDic.Add(msg.tokenUser, new List<string>());

            msg.info = "Demande de déchiffrage reçue";
            msg.appVersion = "1";
            msg.operationVersion = "1";
            for (int index = 0; index < msg.data.Length; index++)
            {
                if (index % 2 == 0)
                    ResultContainer.userAndNamesOfFilesToDecipherDic[msg.tokenUser].Add(msg.data[index].ToString());
                else
                    ResultContainer.userAndContentOfFilesToDecipherDic[msg.tokenUser].Add(msg.data[index].ToString());
            }
            //namesOfFilesToDecipher.Add(msg.data[index].ToString());
            //textsToDecipher.Add(msg.data[index].ToString());


            List<string> listTest = new List<string>();
            listTest.Add("FileName");
            listTest.Add("Content");
            listTest.Add("Key");
            msg.data = listTest.ToArray();
            CreateKey(0, 26);

            //var options = new ParallelOptions() { MaxDegreeOfParallelism = 4 };

            //Parallel.ForEach(listkey, str =>
            //{


            //    List<string> filesDeciphered = new List<string>();
            //    List<string> textsDeciphered = new List<string>();
            //    foreach (string strfile in textsToDecipher)
            //    {
            //        textsDeciphered.Add(ToValidXmlCharactersString(Dechiffrer(str.ToCharArray(), strfile)));
            //    }

            //    for (int index = 0; index < textsDeciphered.Count; index++)
            //    {
            //        filesDeciphered.Add(ResultContainer.userAndNamesOfFilesToDecipherDic[msg.tokenUser].ElementAt(index));
            //        filesDeciphered.Add(textsDeciphered.ElementAt(index));
            //    }
            //    Console.WriteLine(filesDeciphered.ElementAt(1));
            //    filesDeciphered.Add(str);

            //    FacadeEndpoint.FacadeEndpointClient facadeEndpointClient = new FacadeEndpoint.FacadeEndpointClient();
            //    FacadeEndpoint.soapMessage soapMessage = CreateSoapMessageFromMessage(msg, listTest);

            //    msg = CreateMessageFromSoapMessage(facadeEndpointClient.receiveDecipherOrder(soapMessage));
            //});

                    FacadeEndpoint.FacadeEndpointClient facadeEndpointClient = new FacadeEndpoint.FacadeEndpointClient();
                    FacadeEndpoint.soapMessage soapMessage = CreateSoapMessageFromMessage(msg, listTest);

                    msg = CreateMessageFromSoapMessage(facadeEndpointClient.receiveDecipherOrder(soapMessage));



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


        public static string ToValidXmlCharactersString(string s, int startIndex = 0)
        {
            int firstInvalidChar = IndexOfFirstInvalidXMLChar(s, startIndex);
            if (firstInvalidChar < 0)
                return s;

            startIndex = firstInvalidChar;

            int len = s.Length;
            var sb = new StringBuilder(len);

            if (startIndex > 0)
                sb.Append(s, 0, startIndex);

            for (int i = startIndex; i < len; i++)
                if (IsLegalXmlChar(s[i]))
                    sb.Append(s[i]);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the index of the first invalid XML 1.0 character in this string, else returns -1.
        /// </summary>
        /// <param name="s">Xml string.</param>
        /// <param name="startIndex">Start index.</param>
        public static int IndexOfFirstInvalidXMLChar(string s, int startIndex = 0)
        {
            if (s != null && s.Length > 0 && startIndex < s.Length)
            {

                if (startIndex < 0) startIndex = 0;
                int len = s.Length;

                for (int i = startIndex; i < len; i++)
                    if (!IsLegalXmlChar(s[i]))
                        return i;
            }
            return -1;
        }

        /// <summary>
        /// Indicates whether a given character is valid according to the XML 1.0 spec.
        /// This code represents an optimized version of Tom Bogle's on SO: 
        /// https://stackoverflow.com/a/13039301/264031.
        /// </summary>
        public static bool IsLegalXmlChar(char c)
        {
            if (c > 31 && c <= 55295)
                return true;
            if (c < 32)
                return c == 9 || c == 10 || c == 13;
            return (c >= 57344 && c <= 65533) || c > 65535;
            // final comparison is useful only for integral comparison, if char c -> int c, useful for utf-32 I suppose
            //c <= 1114111 */ // impossible to get a code point bigger than 1114111 because Char.ConvertToUtf32 would have thrown an exception
        }


    }
}

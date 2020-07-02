using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract;

namespace Job
{
    static class ResultContainer
    {
        private static Dictionary<string, List<Message>> operationDataList = new Dictionary<string, List<Message>>();
        public static Dictionary<string, List<string>> userAndNamesOfFilesToDecipherDic = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> userAndContentOfFilesToDecipherDic = new Dictionary<string, List<string>>();

        static public void AddDecipherResultToWaitingList(string tokenUser,Message msg)
        {
            if(!operationDataList.ContainsKey(tokenUser))
            {
                operationDataList.Add(msg.tokenUser, new List<Message>());
            }

            operationDataList[tokenUser].Add(msg);
        }

        static public object[] GetAwaitingResultForUser(string tokenUser)
        {
            List<object> dataList = new List<object>();
            foreach (Message msg in  operationDataList[tokenUser])
            {
                for (int i=0 ; i < msg.data.Length ; i++)
                {
                    if((i+1)%4!=0)
                    {
                        dataList.Add(msg.data.ElementAt(i));
                    }
                }
                
            }
            // structure de data doit être --> [0]nom - [1]content - [2]key
            operationDataList.Remove(tokenUser);
            userAndNamesOfFilesToDecipherDic.Remove(tokenUser);
            userAndContentOfFilesToDecipherDic.Remove(tokenUser);

            return dataList.ToArray();
        }

    }
}

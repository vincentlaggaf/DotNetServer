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
        private static Dictionary<string, Message> operationDataList = new Dictionary<string, Message>();
        
        static public void AddDecipherResultToWaitingList(string tokenUser,Message msg)
        {
            operationDataList.Add(tokenUser, msg);
        }

        static public object[] GetAwaitingResultForUser(string tokenUser)
        {
            object [] data= operationDataList[tokenUser].data;
            operationDataList.Remove(tokenUser);

            return data;
        }

    }
}

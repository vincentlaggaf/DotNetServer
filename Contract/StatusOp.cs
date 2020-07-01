using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Contract
{
    [DataContract]
    public enum StatusOp
    {
        [EnumMember]
        Waiting = 0,
        [EnumMember]
        Working = 1,
        [EnumMember]
        Finished = 2,
        [EnumMember]
        Sent = 3

    }
}

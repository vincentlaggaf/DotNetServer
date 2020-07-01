using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    class Authentificator
    {
        public string Authenticate(string login, string password)
        {
            DAO dao = new DAO();

            return dao.CheckLoginInfos(login, password);
        }
    }
}

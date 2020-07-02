using Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    public class DAO 
    {
        public string CheckLoginInfos(string login,string password)
        {
            string tokenUser = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string request = $"SELECT TokenUser FROM Users WHERE Login = '{login}' AND Password = '{password}'";

                SqlCommand sqlCommand = new SqlCommand(request, connection);
                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    if(sqlReader.Read())
                    {
                        tokenUser = String.Format("{0}", sqlReader["TokenUser"]).ToString();
                    }
                }

            }

            return tokenUser;
        }



        public StatusOp GetUserOperationStatus(string tokenUser)
        {
            StatusOp statusOp = StatusOp.Waiting;
            string statusOpReceiver = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string storedProcedureName = "GetRequestStatus";

                SqlCommand sqlCommand = new SqlCommand(storedProcedureName, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@tokenUser", tokenUser));
                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        statusOpReceiver = String.Format("{0}", sqlReader["Request_Status"]).ToString();
                        switch (statusOpReceiver)
                        {
                            case "Working":
                                statusOp = StatusOp.Working;
                                break;
                            case "Finished":
                                statusOp = StatusOp.Finished;
                                break;
                            default:
                                statusOp = StatusOp.Waiting;
                                break;
                        }
                    }
                }

            }

            return statusOp;

        }
        public void CreateNewDecipherRequest(string tokenUser)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string storedProcedureName = "CreateNewRequest";

                SqlCommand sqlCommand = new SqlCommand(storedProcedureName, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@tokenUser", tokenUser));
                sqlCommand.ExecuteNonQuery();

            }
        }

        public void UpdateRequestStatus(StatusOp statusOp, string tokenUser)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string storedProcedureName = "UpdateRequestStatus";

                SqlCommand sqlCommand = new SqlCommand(storedProcedureName, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@requestStatus", statusOp.ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@tokenUser", tokenUser));
                sqlCommand.ExecuteNonQuery();

            }

        }
        public string GetMailFromTokenUser(string tokenUser)
        {
            string mail = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string storedProcedureName = "GetMailFromTokenUser";

                SqlCommand sqlCommand = new SqlCommand(storedProcedureName, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@tokenUser", tokenUser));
                using (SqlDataReader sqlReader = sqlCommand.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        mail = String.Format("{0}", sqlReader["Mail"]).ToString();
                    }
                }

            }

            return mail;
        }
        private SqlConnection GetConnection()
        {
            string connectionString = "Data Source=XAVIER-OKLM-ZER;Initial Catalog=DecipherService;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

    }
}

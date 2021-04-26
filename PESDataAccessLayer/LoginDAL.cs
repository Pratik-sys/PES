using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PESEntity;
using PESExceptions;

namespace PESDataAccessLayer
{
    public class LoginDAL
    {
        /*
        public List<Login> GetAllLoginsDAL()
        {
            List<Login> LoginList = null;
            try
            {
                DbCommand command = DataConnection.CreateCommand();
                command.CommandText = "DisplayLoginCredentials";

                DataTable dataTable = DataConnection.ExecuteSelectCommand(command);
                if (dataTable.Rows.Count > 0)
                {
                    LoginList = new List<Login>();
                    for (int rowCounter = 0; rowCounter < dataTable.Rows.Count; rowCounter++)
                    {
                        Login login = new Login();
                        login.LoginId = (string)dataTable.Rows[rowCounter][1];
                        login.UserId = (string)dataTable.Rows[rowCounter][2];
                        login.Password = (string)dataTable.Rows[rowCounter][3];
                        LoginList.Add(login);
                    }
                }
            }
            catch (DbException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return LoginList;
        }
        */

        SqlConnection objCon = new SqlConnection(PESConfiguration.ConnectionString);
        SqlCommand objCom = null;

        //----------------Customer Sigh Up DAL------------
        public bool SignUpDAL(string custId)
        {
            bool loginCredentialsAdded = false;
            try
            {
                objCom = new SqlCommand("uspSignUp", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@cId", SqlDbType.VarChar).Value = custId;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();
                if (affectedRows > 0)
                {
                    loginCredentialsAdded = true;
                }
            }
            catch (SqlException ex)
            {
                string errormessage;
                switch (ex.ErrorCode)
                {
                    case -2146232060:
                        errormessage = "Database Does NotExists Or AccessDenied";
                        break;
                    default:
                        errormessage = ex.Message;
                        break;
                }
            }
            return loginCredentialsAdded;
        }

        //----------------Verify Login Credentials DAL-----------------
        public bool checkLoginCredentialsDAL(Login login)
        {
            bool loggedCustomer = false;
            try
            {
                objCom = new SqlCommand("uspCheckLoginCredentials", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@password", SqlDbType.VarChar).Value = login.Password;
                objCom.Parameters.Add("@userId", SqlDbType.VarChar).Value = login.UserId;
                objCom.Parameters.Add("@loginType", SqlDbType.VarChar).Value = login.LoginType;

                objCon.Open();
                int affectedRows = (int)objCom.ExecuteScalar();
                objCon.Close();
                if (affectedRows > 0)
                {
                    loggedCustomer = true;
                }
            }
            catch (SqlException ex)
            {
                string errormessage;
                switch (ex.ErrorCode)
                {
                    case -2146232060:
                        errormessage = "Database Does NotExists Or AccessDenied";
                        break;
                    default:
                        errormessage = ex.Message;
                        break;
                }
            }
            return loggedCustomer;
        }

        //----------------Get Current Login Details of a specific customer DAL---------
        public Login GetCurrentLoginDAL()
        {
            Login login = null;
            try
            {
                objCom = new SqlCommand("uspGetCurrentLoginCredentials", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                SqlDataReader dataReader = objCom.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                
                if (dataTable.Rows.Count > 0)
                {
                    login = new Login();
                    login.UserId = (string)dataTable.Rows[0][1];
                    login.Password = (string)dataTable.Rows[0][2];
                }
                dataReader.Close();
                objCon.Close();
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return login;
        }

        //-----------------Find Admin DAL-------------
        public bool CheckAdminDAL()
        {
            bool checkAdmin = false;
            try
            {
                objCom = new SqlCommand("uspFindAdmin", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                int affectedRows = (int)objCom.ExecuteScalar();
                objCon.Close();
                if (affectedRows > 0)
                {
                    checkAdmin = true;
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return checkAdmin;
        }

        //-----------------Create Admin DAL------------
        public bool CreateAdminDAL()
        {
            bool adminCreated = false;
            try
            {
                objCom = new SqlCommand("uspCreateAdmin", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();
                if (affectedRows > 0)
                {
                    adminCreated = true;
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return adminCreated;
        }
    }
}

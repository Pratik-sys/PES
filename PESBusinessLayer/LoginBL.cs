using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PESExceptions;
using PESEntity;
using System.Text.RegularExpressions;
using PESDataAccessLayer;

namespace PESBusinessLayer
{
    public class LoginBL
    {
        //----------------Validation for Login Credentials-------------
        private static bool ValidateLoginCredentials(Login login)
        {
            StringBuilder sb = new StringBuilder();
            bool ValidLogin = true;
            var regexItem = new Regex("^[A-Z0-9]*$");

            if (login.UserId == string.Empty ||  login.UserId.Length != 7 ||!regexItem.IsMatch(login.UserId))
            {
                ValidLogin = false;
                sb.Append(Environment.NewLine + "Invalid User ID");
            }
            if (login.Password == string.Empty || login.Password.Length != 7 || !regexItem.IsMatch(login.Password))
            {
                ValidLogin = false;
                sb.Append(Environment.NewLine + "Password Required");
            }
            if (ValidLogin == false)
                throw new PolicyExceptions(sb.ToString());
            return ValidLogin;
        }

        //----------------Customer Sigh Up BL------------
        public static bool SignUpBL(string custId)
        {
            bool custSignUp = false;
            try
            {
                LoginDAL loginDAL = new LoginDAL();
                custSignUp = loginDAL.SignUpDAL(custId);
            }
            catch (PolicyExceptions)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return custSignUp;
        }

        //----------------Verify Login Credentials BL-----------------
        public static bool checkLoginCredentialsBL(Login login)
        {
            bool custSignIn = false;
            try
            {
                if (ValidateLoginCredentials(login))
                {
                    LoginDAL loginDAL = new LoginDAL();
                    custSignIn = loginDAL.checkLoginCredentialsDAL(login);
                }
            }
            catch (PolicyExceptions)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return custSignIn;
        }

        //----------------Get Current Login Details of a specific customer BL---------
        public static Login GetCurrentLoginBL()
        {
            Login getCurrentLogin = null;
            try
            {
                LoginDAL loginDAL = new LoginDAL();
                getCurrentLogin = loginDAL.GetCurrentLoginDAL();
            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return getCurrentLogin;
        }

        //-----------------Find Admin BL-------------
        public static bool CheckAdminBL()
        {
            bool checkAdmin = false;
            try
            {
                LoginDAL loginDAL = new LoginDAL();
                checkAdmin = loginDAL.CheckAdminDAL();
            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return checkAdmin;
        }

        //-----------------Create Admin BL
        public static bool CreateAdminBL()
        {
            bool adminCreated = false;
            try
            {
                LoginDAL loginDAL = new LoginDAL();
                adminCreated = loginDAL.CreateAdminDAL();
            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return adminCreated;
        }
    }
}

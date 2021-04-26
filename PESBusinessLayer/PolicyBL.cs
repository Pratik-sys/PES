using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PESExceptions;
using PESEntity;
using System.Text.RegularExpressions;
using PESDataAccessLayer;
using System.Data;

namespace PESBusinessLayer
{
    public class PolicyBL
    {
        //-------------- Validate Policy Details ---------------
        private static bool ValidatePolicy(Policy policy)
        {
            StringBuilder sb = new StringBuilder();
            bool validPolicy = true;
            var regexAlpha = new Regex(@"^[A-Za-z\s]+$");
            var regexFilePath = new Regex(@"((?:[^/]*/)*)(.*)");

            if (policy.Nominee == string.Empty || !regexAlpha.IsMatch(policy.Nominee))
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Nominee cannot be empty");
            }
            if (policy.PremiumPayment == string.Empty)
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Premium Payment cannot be empty");
            }
            if (policy.Relation == string.Empty)
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Relation cannot be empty");
            }
            if (policy.FilePath == string.Empty || !regexFilePath.IsMatch(policy.FilePath))
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Filepath is invalid");
            }
            if(validPolicy == false)
                throw new PolicyExceptions(sb.ToString());
            return validPolicy;
        }

        // ------ Validation For Policy ID  ------
        public static bool validateId(string id)
        {
            bool isValid = true;
            var regexItem = new Regex("^[A-Z0-9]*$");
            StringBuilder sb = new StringBuilder();

            if (id == string.Empty || id.Length != 7 || !regexItem.IsMatch(id))
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Enter Valid Id");
            }
            if (isValid == false)
                throw new PolicyExceptions(sb.ToString());
            return isValid;
        }

        //-------------- Validate Update Policy Details ---------------
        private static bool ValidateUpdatedPolicyDetails(Policy policy)
        {
            StringBuilder sb = new StringBuilder();
            bool validPolicy = true;

            var regexNumeric = new Regex("^[0-9]*$");
            var regexAlpha = new Regex(@"^[A-Za-z\s]+$");
            var regexFilePath = new Regex(@"((?:[^/]*/)*)(.*)");

            if (policy.Nominee == string.Empty || !regexAlpha.IsMatch(policy.Nominee))
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Nominee cannot be empty");
            }
            if (policy.PremiumPayment == string.Empty)
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Premium Payment cannot be empty");
            }
            if (policy.Relation == string.Empty)
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Relation cannot be empty");
            }
            if (policy.FilePath == string.Empty || !regexFilePath.IsMatch(policy.FilePath))
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Filepath is invalid");
            }

            if (policy.C.Address == string.Empty)
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Invalid Address");

            }
            if (policy.C.Telephone == string.Empty || !regexNumeric.IsMatch(policy.C.Telephone) || policy.C.Telephone.Length != 10)
            {
                validPolicy = false;
                sb.Append(Environment.NewLine + "Invalid Telephone number");
            }
            if (validPolicy == false)
                throw new PolicyExceptions(sb.ToString());
            return validPolicy;
        }

        //------ Validation For Customer Name And Customer DOB ------ 
        public static bool validateNameAndDob(string name, DateTime dob)
        {
            bool isValid = true;
            var regexAlpha = new Regex(@"^[A-Za-z\s]+$");
            StringBuilder sb = new StringBuilder();
            if (name == string.Empty || !regexAlpha.IsMatch(name))
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Enter Valid Name");
            }
            if (DateTime.Compare(dob, DateTime.Today) >=0)
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Enter Valid DOB");
            }
            if (isValid == false)
                throw new PolicyExceptions(sb.ToString());
            return isValid;
        }

        //----------------ADD Policy BL-----------------
        public static bool AddPolicyBL(Policy newPolicy)
        {
            bool policyAdded = false;
            try
            {
                if (ValidatePolicy(newPolicy))
                {
                    PolicyDAL policyDAL = new PolicyDAL();
                    policyAdded = policyDAL.AddPolicyDAL(newPolicy);
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
            return policyAdded;
        }

        //------------- Search Policy By Policy Number OR by Customer ID BL------------
        public static DataSet SearchPolicyBL(string ID)
        {
            DataSet searchPolicy = null;
            try
            {
                if (validateId(ID))
                {
                    PolicyDAL policyDAL = new PolicyDAL();
                    searchPolicy = policyDAL.SearchPolicyDAL(ID);
                }

            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return searchPolicy;

        }

        //------------- Search Policy By Customer Name And DOB BL------------
        public static DataSet SearchPolicyBL(string Name, DateTime Dob)
        {
            DataSet searchPolicy = null;
            try
            {
                if (validateNameAndDob(Name, Dob))
                {
                    PolicyDAL policyDAL = new PolicyDAL();
                    searchPolicy = policyDAL.SearchPolicyDAL(Name, Dob);
                }

            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return searchPolicy;

        }

        //------------- Display All Policy BL------------
        public static DataSet GetAllPolicyBL()
        {
            DataSet policyList = null;
            try
            {
                PolicyDAL policyDAL = new PolicyDAL();
                policyList = policyDAL.GetAllPolicyDAL();
            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return policyList;
        }

        //------------- Display All Policy by Customer Id and Policy Number for a particualr customer BL------------
        public static DataSet GetAllPolicyByCustomerIdAndPolicyNumberBL(string custId, string policyNo)
        {
            DataSet policyList = null;
            try
            {
                if (validateId(policyNo))
                {
                    PolicyDAL policyDAL = new PolicyDAL();
                    policyList = policyDAL.GetAllPolicyByCustomerIdAndPolicyNumberDAL(custId, policyNo);
                }
            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return policyList;
        }

        //------------- View Specific Policy BL------------
        public static Policy ViewPolicyBL(string policyNum)
        {
            Policy getPolicy = null;
            try
            {
                if (validateId(policyNum))
                {
                    PolicyDAL policyDAL = new PolicyDAL();
                    getPolicy = policyDAL.ViewPolicyDAL(policyNum);
                }

            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return getPolicy;
        }

        //-------------Update Specific policy BL------------
        public static bool UpdatePolicy(Policy updatePolicy)
        {
            bool policyUpdated = false;
            try
            {
                if (ValidateUpdatedPolicyDetails(updatePolicy))
                {
                    PolicyDAL policydal = new PolicyDAL();
                    policyUpdated = policydal.updatePolicyDAL(updatePolicy);
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

            return policyUpdated;
        }
    }
}

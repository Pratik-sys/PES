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
    public class PolicyDAL
    {
        SqlConnection objCon = new SqlConnection(PESConfiguration.ConnectionString);
        SqlCommand objCom = null;

        //----------------ADD Policy DAL-----------------
        public bool AddPolicyDAL(Policy newPolicy)
        {
            bool policyAdded = false;
            try
            {
                objCom = new SqlCommand("uspApplyForPolicy", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@custId", SqlDbType.VarChar).Value = newPolicy.C.CustomerId;
                objCom.Parameters.Add("@productId", SqlDbType.VarChar).Value = newPolicy.P.ProductId;
                objCom.Parameters.Add("@policyNom", SqlDbType.VarChar).Value = newPolicy.Nominee;
                objCom.Parameters.Add("@policyNomRelation", SqlDbType.VarChar).Value = newPolicy.Relation;
                objCom.Parameters.Add("@policyPremiumPaymentFreqn", SqlDbType.VarChar).Value = newPolicy.PremiumPayment;
                objCom.Parameters.Add("@filePath", SqlDbType.VarChar).Value = newPolicy.FilePath;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();

                if (affectedRows > 0)
                    policyAdded = true;

            }
            catch (Exception ex)
            {
               
                //string errormessage;

                //switch (ex.ErrorCode)
                //{
                //    case -2146232060:
                //        errormessage = "Database Does NotExists Or AccessDenied";
                //        break;
                //    default:
                //        errormessage = ex.Message;
                //        break;
                //}
                throw new PolicyExceptions(ex.Message);
               
            }

            return policyAdded;
        }

        //------------- Search Policy By Policy Number OR by Customer ID DAL------------
        public DataSet SearchPolicyDAL(string ID)
        {
            DataSet searchPolicy = null;
            try
            {
                if (ID.Substring(0, 2) == "PN")
                {
                    objCom = new SqlCommand("uspSearchByPolicyNumber", objCon);
                    objCom.CommandType = CommandType.StoredProcedure;
                    objCom.Parameters.Add("@policyNumber", SqlDbType.VarChar).Value = ID;
                }
                else if(ID.Substring(0, 2) == "CN")
                {
                    objCom = new SqlCommand("uspSearchByCustomerId", objCon);
                    objCom.CommandType = CommandType.StoredProcedure;
                    objCom.Parameters.Add("@customerId", SqlDbType.VarChar).Value = ID;
                }
                else
                {
                    throw new PolicyExceptions("No Records Found");
                }
                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    searchPolicy = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Records Found");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            finally
            {
                if (objCom.Connection.State == ConnectionState.Open)
                    objCon.Close();
            }
            return searchPolicy;
        }

        //------------- Search Policy By Customer Name And DOB DAL------------
        public DataSet SearchPolicyDAL(string Name, DateTime DOB)
        {
            DataSet searchPolicy = null;
            try
            {
                objCom = new SqlCommand("uspSearchByCustomerNameAndDob", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@customerName", SqlDbType.VarChar).Value = Name;
                objCom.Parameters.Add("@customerDOB", SqlDbType.DateTime).Value = DOB;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    searchPolicy = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Records Found");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            finally
            {
                if (objCom.Connection.State == ConnectionState.Open)
                    objCon.Close();
            }
            return searchPolicy;
        }

        //------------- Display All Policy DAL------------
        public DataSet GetAllPolicyDAL()
        {
            DataSet policyList = null;

            try
            {
                objCom = new SqlCommand("uspGetAllPolicy", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    policyList = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Policies found at present");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            finally
            {
                if (objCom.Connection.State == ConnectionState.Open)
                    objCon.Close();
            }
            return policyList;
        }

        //------------- Display All Policy by Customer Id and Policy Number for a particualr customer DAL------------
        public DataSet GetAllPolicyByCustomerIdAndPolicyNumberDAL(string custId, string policyNo)
        {
            DataSet policyList = null;
            try
            {
                objCom = new SqlCommand("uspSearchPolicyByCustomerIdAndPolicyNumber", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@cId", SqlDbType.VarChar).Value = custId;
                objCom.Parameters.Add("@pN", SqlDbType.VarChar).Value = policyNo;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    policyList = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Records Found");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            finally
            {
                if (objCom.Connection.State == ConnectionState.Open)
                    objCon.Close();
            }
            return policyList;
        }

        //------------- View Specific Policy DAL------------
        public Policy ViewPolicyDAL(string policyNum)
        {
            Policy policy = null;
            try
            {
                objCom = new SqlCommand("uspViewPolicy", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@pN", SqlDbType.VarChar).Value = policyNum;

                objCon.Open();
                SqlDataReader dataReader = objCom.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                

                if (dataTable.Rows.Count > 0)
                {
                    policy = new Policy();
                    policy.C = new Customer()
                    {
                        CustomerId = (string)dataTable.Rows[0][0],
                        CustomerName = (string)dataTable.Rows[0][1],
                        Address = (string)dataTable.Rows[0][2],
                        Gender = Convert.ToChar(dataTable.Rows[0][3].ToString()),
                        Telephone = (string)dataTable.Rows[0][4],
                        DOB = Convert.ToDateTime(dataTable.Rows[0][5].ToString()),
                        Smoker = Convert.ToBoolean(dataTable.Rows[0][14].ToString())
                    };
                    policy.P = new InsuranceProducts()
                    {
                        ProductId = (string)dataTable.Rows[0][6],
                        ProductName = (string)dataTable.Rows[0][7],
                        Line = (string)dataTable.Rows[0][8]
                    };
                    policy.PolicyId = (string)dataTable.Rows[0][9];
                    policy.Nominee = (string)dataTable.Rows[0][10];
                    policy.Relation = (string)dataTable.Rows[0][11];
                    policy.PremiumPayment = (string)dataTable.Rows[0][12];
                    policy.FilePath = (string)dataTable.Rows[0][13];
                }
                else
                {
                    throw new PolicyExceptions("No Records Found");
                }
                dataReader.Close();
                objCon.Close();
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            finally
            {
                if (objCom.Connection.State == ConnectionState.Open)
                    objCon.Close();
            }
            return policy;
        }

        //-------------Update Specific policy DAL------------
        public bool updatePolicyDAL(Policy updatePolicy)
        {
            bool policyUpdated = false;
            try
            {
                objCom = new SqlCommand("uspUpdatePolicy", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@custId", SqlDbType.VarChar).Value = updatePolicy.C.CustomerId;
                objCom.Parameters.Add("@policyNum", SqlDbType.VarChar).Value = updatePolicy.PolicyId;
                objCom.Parameters.Add("@policyNom", SqlDbType.VarChar).Value = updatePolicy.Nominee;
                objCom.Parameters.Add("@policyNomRelation", SqlDbType.VarChar).Value = updatePolicy.Relation;
                objCom.Parameters.Add("@policyPremiumPaymentFreqn", SqlDbType.VarChar).Value = updatePolicy.PremiumPayment;
                objCom.Parameters.Add("@filePath", SqlDbType.VarChar).Value = updatePolicy.FilePath;
                objCom.Parameters.Add("@custAddress", SqlDbType.VarChar).Value = updatePolicy.C.Address;
                objCom.Parameters.Add("@custTelephone", SqlDbType.VarChar).Value = updatePolicy.C.Telephone;
                objCom.Parameters.Add("@custSmoker", SqlDbType.Bit).Value = updatePolicy.C.Smoker;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();

                if (affectedRows > 0)
                    policyUpdated = true;
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return policyUpdated;
        }
    }
}

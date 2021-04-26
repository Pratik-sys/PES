using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using PESEntity;
using PESExceptions;
using System.Data.Common;

namespace PESDataAccessLayer
{
    public class CustomersDAL
    {
        SqlConnection objCon = new SqlConnection(PESConfiguration.ConnectionString);
        SqlCommand objCom = null;

        //------------- Add Customer DAL ------------
        public Tuple<bool, string> AddCustomersDAL(Customer newCustomer)
        {
            bool customerAdded = false;
            string custId = null;
            try
            {
                objCom = new SqlCommand("uspCreateCustomer", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@custName", SqlDbType.VarChar).Value = newCustomer.CustomerName;
                objCom.Parameters.Add("@custAddress", SqlDbType.VarChar).Value = newCustomer.Address;
                objCom.Parameters.Add("@custTelephone", SqlDbType.VarChar).Value = newCustomer.Telephone;
                objCom.Parameters.Add("@custGender", SqlDbType.Char).Value = newCustomer.Gender;
                objCom.Parameters.Add("@custDOB", SqlDbType.DateTime).Value = newCustomer.DOB;
                objCom.Parameters.Add("@custSmoker", SqlDbType.Bit).Value = newCustomer.Smoker;
                objCom.Parameters.Add("@custHobbies", SqlDbType.VarChar).Value = newCustomer.Hobbies;
                objCom.Parameters.Add("@custId", SqlDbType.VarChar, 7).Direction = ParameterDirection.Output;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                newCustomer.CustomerId = Convert.ToString(objCom.Parameters["@custId"].Value);
                objCon.Close();
                if (affectedRows > 0)
                {
                    customerAdded = true;
                    custId = newCustomer.CustomerId;
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
            return Tuple.Create(customerAdded, custId);
        }

        //-------------- Delete Customer DAL--------------
        public bool DeleteCustDAL(string custId)
        {
            bool custDeleted = false;
            try
            {
                objCom = new SqlCommand("uspDeleteCustomer", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@cId", SqlDbType.VarChar).Value = custId;
                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();

                if (affectedRows > 0)
                    custDeleted = true;
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return custDeleted;
        }

        //------------- Get Customer by Id DAL ------------
        public DataSet GetCustomerByIdDAL(string custId)
        {
            DataSet customer = null;
            try
            {
                objCom = new SqlCommand("uspSearchCustomerById", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@cId", SqlDbType.VarChar).Value = custId;
                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if(dataSet.Tables[0].Rows.Count > 0)
                {
                    customer = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Record Found");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return customer;
        }

        //------------- Search Customer by Name and DOB DAL ------------
        public DataSet SearchCustomerByNameAndDOBDAL(string customerName, DateTime customerDOB)
        {
            DataSet customer = null;
            try
            {
                objCom = new SqlCommand("uspSearchCustomerByNameAndDob", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@cName", SqlDbType.VarChar).Value = customerName;
                objCom.Parameters.Add("@cDOB", SqlDbType.DateTime).Value = customerDOB;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    customer = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Record Found");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return customer;
        }

        //------------- Get All Customers DAL-----------
        public DataSet GetAllCustomersDAL()
        {
            DataSet customerList = null;
            try
            {
                objCom = new SqlCommand("uspGetAllCustomers", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    customerList = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Record Found");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return customerList;
        }
    }
}

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
    public class CustomerBL
    {
        //------------- Validate Customer Details-----------
        private static bool ValidateCustomer(Customer customer)
        {
            StringBuilder sb = new StringBuilder();
            bool ValidCustomer = true;
            var regexNumeric = new Regex("^[0-9]*$");
            var regexAlpha = new Regex(@"^[A-Za-z\s]+$");

            if (customer.CustomerName == string.Empty || !regexAlpha.IsMatch(customer.CustomerName))
            {
                ValidCustomer = false;
                sb.Append(Environment.NewLine + "Invalid Customer Name");

            }
            if(customer.Address == string.Empty)
            {
                ValidCustomer = false;
                sb.Append(Environment.NewLine + "Invalid Address");

            }
            if (customer.Telephone == string.Empty || !regexNumeric.IsMatch(customer.Telephone) || customer.Telephone.Length != 10)
            {
                ValidCustomer = false;
                sb.Append(Environment.NewLine + "Invalid Telephone number");

            }
            if (Convert.ToString(customer.Gender) == string.Empty)
            {
                ValidCustomer = false;
                sb.Append(Environment.NewLine + "Please select gender");

            }
            if (DateTime.Compare(customer.DOB, DateTime.Today) >= 0)
            {
                ValidCustomer = false;
                sb.Append(Environment.NewLine + "Invalid DOB");
            }
            if (customer.Hobbies == string.Empty)
            {
                ValidCustomer = false;
                sb.Append(Environment.NewLine + "Hobbies can't be empty");
            }

            if (ValidCustomer == false)
                throw new PolicyExceptions(sb.ToString());
            return ValidCustomer;
        }

        //------------- Validate Customer ID-----------
        private static bool ValidateCustomerId(string id)
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

        //------------- Validate Customer Name and DOB-----------
        private static bool ValidateCustomerDOBAndName(string name, DateTime dob)
        {
            bool isValid = true;
            var regexAlpha = new Regex(@"^[A-Za-z\s]+$");

            StringBuilder sb = new StringBuilder();

            if (name == string.Empty || !regexAlpha.IsMatch(name))
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Invalid Customer Name");
            }
            if (DateTime.Compare(dob, DateTime.Today) >= 0)
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Invalid DOB");
            }
            if (isValid == false)
                throw new PolicyExceptions(sb.ToString());
            return isValid;
        }

        //------------- Add Customer BL ------------
        public static Tuple<bool,string> AddCustomerBL(Customer newCustomer)
        {
            bool customerAddedflag = false;
            string custId = null;
            try
            {
                if (ValidateCustomer(newCustomer))
                {
                    CustomersDAL customerDAL = new CustomersDAL();
                    var customerAdded = customerDAL.AddCustomersDAL(newCustomer);
                    customerAddedflag = customerAdded.Item1;
                    if (customerAddedflag)
                        custId = customerAdded.Item2;
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

            return Tuple.Create(customerAddedflag, custId);
        }

        //-------------- Delete Customer BL--------------
        public static bool DeletCustomerBL(string deleteCustId)
        {
            bool custDeleted = false;
            try
            {
                if (deleteCustId.Length > 0)
                {
                    CustomersDAL custdDAL = new CustomersDAL();
                    custDeleted = custdDAL.DeleteCustDAL(deleteCustId);
                }
                else
                {
                    throw new PolicyExceptions("Invalid Customer ID");
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

            return custDeleted;
        }

        //------------- Get Customer by Id BL ------------
        public static DataSet GetCustomerByIdBL(string custId)
        {
            DataSet customer = null;
            try
            {
                if (ValidateCustomerId(custId))
                {
                    CustomersDAL customersDAL = new CustomersDAL();
                    customer = customersDAL.GetCustomerByIdDAL(custId);
                }
                else
                {
                    throw new PolicyExceptions("Customer not fount");
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
            return customer;
        }

        //------------- Search Customer by Name and DOB BL ------------
        public static DataSet SearchCustomerByNameAndDOBBL(string customerName, DateTime customerDOB)
        {
            DataSet customer = null;
            try
            {
                if (ValidateCustomerDOBAndName(customerName, customerDOB))
                {
                    CustomersDAL customersDAL = new CustomersDAL();
                    customer = customersDAL.SearchCustomerByNameAndDOBDAL(customerName, customerDOB);
                }
                else
                {
                    throw new PolicyExceptions("Customer not fount");
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
            return customer;
        }

        //------------- Get All Customers BL-----------
        public static DataSet GetAllCustomersBL()
        {
            DataSet customerList = null;
            try
            {
                CustomersDAL customersDAL = new CustomersDAL();
                customerList = customersDAL.GetAllCustomersDAL();
            }
            catch (PolicyExceptions)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerList;
        }

    }
}

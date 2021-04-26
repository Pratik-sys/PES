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
    public class InsuranceProductsBL
    {
        //-----------------Validation for Insurance Product Details ---------------
        private static bool ValidInsuranceProduct(InsuranceProducts insuranceProduct)
        {
            StringBuilder sb = new StringBuilder();
            bool validProduct = true;
            var regexAlphaNumeric = new Regex(@"^[A-Za-z0-9\s]+$");
            if (insuranceProduct.ProductName == string.Empty || !regexAlphaNumeric.IsMatch(insuranceProduct.ProductName))
            {
                validProduct = false;
                sb.Append(Environment.NewLine + "Product Name cannot be empty or contain special characters.");
            }
            if (validProduct == false)
                throw new PolicyExceptions(sb.ToString());
            return validProduct;
        }

        //-----------------Validation for Update Insurance Product Details ---------------
        private static bool ValidateUpdatedInsuranceProduct(InsuranceProducts insuranceProduct)
        {
            bool isValid = true;
            StringBuilder sb = new StringBuilder();
            var regexAlphaNumeric1 = new Regex(@"^[A-Za-z0-9\s]+$");
            var regexAlphaNumeric2 = new Regex("^[A-Z0-9]*$");
            if (insuranceProduct.ProductId == string.Empty || insuranceProduct.ProductId.Substring(0, 2) != "PI" || insuranceProduct.ProductId.Length !=7 || !regexAlphaNumeric2.IsMatch(insuranceProduct.ProductId))
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Product Id cannot be empty or contain special characters.");
            }
            if (insuranceProduct.ProductName == string.Empty || !regexAlphaNumeric1.IsMatch(insuranceProduct.ProductName))
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Product Name cannot be empty or contain special characters.");
            }
            if (isValid == false)
                throw new PolicyExceptions(sb.ToString());
            return isValid;
        }

        //----------------Validation for Product Id-------------
        private static bool ValidateID(string id)
        {
            bool isValid = true;
            StringBuilder sb = new StringBuilder();
            var regexAlphaNumeric = new Regex("^[A-Z0-9]*$");
            if (id == string.Empty || id.Length != 7 || !regexAlphaNumeric.IsMatch(id))
            {
                isValid = false;
                sb.Append(Environment.NewLine + "Product Not Found");
            }
            if (isValid == false)
                throw new PolicyExceptions(sb.ToString());
            return isValid;
        }

        //----------------Add Insurance Product BL-------------
        public static bool AddInsuranceProductBL(InsuranceProducts newInsuranceProduct)
        {
            bool insuranceProductAdded = false;
            try
            {
                if (ValidInsuranceProduct(newInsuranceProduct))
                {
                    InsuranceProductDAL insuranceProductDAL = new InsuranceProductDAL();
                    insuranceProductAdded = insuranceProductDAL.AddInsuranceProductDAL(newInsuranceProduct);
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
            return insuranceProductAdded;
        }

        //----------------Get All Insuance Product BL-------------
        public static DataSet GetAllInsuranceProductsBL()
        {
            DataSet insuranceProductlist = null;
            try
            {
                InsuranceProductDAL insuranceProductDAL = new InsuranceProductDAL();
                insuranceProductlist = insuranceProductDAL.GetAllInsuredProductsDAL();
            }
            catch (PolicyExceptions ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return insuranceProductlist;
        }

        //----------------Update Insurance Product BL-------------
        public static bool UpdateInsuranceProductBL(InsuranceProducts updateInsuraceProduct)
        {
            bool insuranceProductUpdated = false;
            try
            {
                if (ValidateUpdatedInsuranceProduct(updateInsuraceProduct))
                {
                    InsuranceProductDAL insuranceProductDAL = new InsuranceProductDAL();
                    insuranceProductUpdated = insuranceProductDAL.UpdateInsuranceProductDAL(updateInsuraceProduct);
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

            return insuranceProductUpdated;
        }

        //----------------Search Insurance Product BL-------------
        public static InsuranceProducts SearchInsuranceProductBL(string productId)
        {
            InsuranceProducts searchProduct = null;
            try
            {
                if (ValidateID(productId))
                {
                    InsuranceProductDAL insuranceProductDAL = new InsuranceProductDAL();
                    searchProduct = insuranceProductDAL.SearchInsuranceProductDAL(productId);
                    if(searchProduct == null)
                    {
                        throw new PolicyExceptions("No Records Found");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return searchProduct;
        } 
    }
}

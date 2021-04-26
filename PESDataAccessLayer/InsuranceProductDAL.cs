using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PESExceptions;
using PESEntity;
using System.Data.SqlClient;

namespace PESDataAccessLayer
{
    public class InsuranceProductDAL
    {
        SqlConnection objCon = new SqlConnection(PESConfiguration.ConnectionString);
        SqlCommand objCom = null;

        //----------------Add Insurance Product BL-------------
        public bool AddInsuranceProductDAL(InsuranceProducts newInsuranceProducts)
        {
            bool insuredProductedAdded = false;
            try
            {
                objCom = new SqlCommand("uspInsertInsuranceProduct", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@productName", SqlDbType.VarChar).Value = newInsuranceProducts.ProductName;
                objCom.Parameters.Add("@productLine", SqlDbType.VarChar).Value = newInsuranceProducts.Line;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();
                if (affectedRows > 0)
                {
                    insuredProductedAdded = true;

                }
            }
            catch (SqlException ex)
            {
                string errormessage;
                errormessage = ex.Message;
                throw new PolicyExceptions(errormessage);
            }
            return insuredProductedAdded;

        }

        //----------------Get All Insuance Product BL-------------
        public DataSet GetAllInsuredProductsDAL()
        {
            DataSet insuaranceProductsList = null;
            try
            {
                objCom = new SqlCommand("uspDisplayInsuranceProducts", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    insuaranceProductsList = dataSet;
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
            return insuaranceProductsList;
        }

        //----------------Update Insurance Product BL-------------
        public bool UpdateInsuranceProductDAL(InsuranceProducts updateInsuranceProducts)
        {
            bool insuranceProductsUpdated = false;
            try
            {
                objCom = new SqlCommand("uspUpdateInsuranceProduct", objCon);
                objCom.CommandType = CommandType.StoredProcedure;
                
                objCom.Parameters.Add("@productId", SqlDbType.VarChar).Value = updateInsuranceProducts.ProductId;
                objCom.Parameters.Add("@productName", SqlDbType.VarChar).Value = updateInsuranceProducts.ProductName;
                objCom.Parameters.Add("@productLine", SqlDbType.VarChar).Value = updateInsuranceProducts.Line;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();
                if (affectedRows > 0)
                {
                    insuranceProductsUpdated = true;
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
            return insuranceProductsUpdated;

        }

        //----------------Search Insurance Product BL-------------
        public InsuranceProducts SearchInsuranceProductDAL(string productId)
        {
            InsuranceProducts searchProduct = null;
            try
            {
                objCom = new SqlCommand("uspSearchInsuranceProduct", objCon);
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.Parameters.Add("@insuranceId", SqlDbType.VarChar).Value = productId;

                objCon.Open();
                SqlDataReader dataReader = objCom.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                if (dataTable.Rows.Count > 0)
                {
                    searchProduct = new InsuranceProducts();
                    searchProduct.ProductId = dataTable.Rows[0][1].ToString();
                    searchProduct.ProductName = dataTable.Rows[0][2].ToString();
                    searchProduct.Line = dataTable.Rows[0][3].ToString();
                }
                else
                {
                    throw new PolicyExceptions("No Record Found");
                }
                dataReader.Close();
                objCon.Close();
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return searchProduct;
        }
    }
}

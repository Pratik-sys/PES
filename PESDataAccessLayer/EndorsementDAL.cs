using PESEntity;
using PESExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PESDataAccessLayer
{
    public class EndorsementDAL
    {
        SqlConnection objCon = new SqlConnection(PESConfiguration.ConnectionString);
        SqlCommand objCom = null;

        //---------- Update Endorsement DAL-------------
        public bool UpdateEndorsementDAL(Endorsement newEndorsement, string flag)
        {
            bool endorsementUpdated = false;
            try
            {

                objCom = new SqlCommand("uspEndorsementApproval", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@eId", SqlDbType.VarChar).Value = newEndorsement.EndorsementId;
                objCom.Parameters.Add("@pN", SqlDbType.VarChar).Value = newEndorsement.PolicyId;
                objCom.Parameters.Add("@flag", SqlDbType.VarChar).Value = flag;

                objCon.Open();
                int affectedRows = objCom.ExecuteNonQuery();
                objCon.Close();
                if (affectedRows > 0)
                {
                    endorsementUpdated = true;
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
            return endorsementUpdated;
        }

        //---------- Get All Endorsements DAL-------------
        public DataSet getEndorsementDAL()
        {
            DataSet endorsementList = null;
            try
            {
                objCom = new SqlCommand("uspGetAllEndorsements", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    endorsementList = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Endorsements found at present");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return endorsementList;
        }

        //---------- Get All Endorsement Status DAL-------------
        public DataSet getEndorsementStatusDAL(string custId)
        {
            DataSet endorsementStatusList = null;
            try
            {
                objCom = new SqlCommand("uspGetAllEndorsementsStatus", objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                objCom.Parameters.Add("@cId", SqlDbType.VarChar).Value = custId;

                objCon.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(objCom);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objCon.Close();

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    endorsementStatusList = dataSet;
                }
                else
                {
                    throw new PolicyExceptions("No Updates from Admin found at present");
                }
            }
            catch (SqlException ex)
            {
                throw new PolicyExceptions(ex.Message);
            }
            return endorsementStatusList;
        }
    }
}

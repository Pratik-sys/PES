using PESDataAccessLayer;
using PESEntity;
using PESExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PESBusinessLayer
{
    public class EndorsementBL
    {
        //---------- Update Endorsement BL-------------
        public static bool UpdateEndorsemetBL(Endorsement endorsement, string flag)
        {
            bool endorsementUpdated = false;
            try
            {
                EndorsementDAL endorsementDAL = new EndorsementDAL();
                endorsementUpdated = endorsementDAL.UpdateEndorsementDAL(endorsement, flag);
            }
            catch (PolicyExceptions)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return endorsementUpdated;
        }

        //---------- Get All Endorsements BL-------------
        public static DataSet GetAllEndorsement()
        {
            DataSet endorsementList = null;
            try
            {
                EndorsementDAL endorsementDAL = new EndorsementDAL();
                endorsementList = endorsementDAL.getEndorsementDAL();
            }
            catch (PolicyExceptions)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return endorsementList;
        }

        //---------- Get All Endorsement Status BL-------------
        public static DataSet GetAllEndorsementStatus(string custId)
        {
            DataSet endorsementStatusList = null;
            try
            {
                EndorsementDAL endorsementDAL = new EndorsementDAL();
                endorsementStatusList = endorsementDAL.getEndorsementStatusDAL(custId);
            }
            catch (PolicyExceptions)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return endorsementStatusList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PESDataAccessLayer
{
    public class PESConfiguration
    {

        private static string connectionString;

        public static string ConnectionString
        {
            get { return PESConfiguration.connectionString; }
            set { PESConfiguration.connectionString = value; }
        }

        static PESConfiguration()
        {
            connectionString = ConfigurationManager.ConnectionStrings["PolicyEndorsementSystem"].ConnectionString;
        }
    }
}

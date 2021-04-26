using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PESExceptions
{
    public class PolicyExceptions : ApplicationException
    {
        public PolicyExceptions()
            : base()
        {

        }

        public PolicyExceptions(string message)
            : base(message)
        {

        }

        public PolicyExceptions(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}

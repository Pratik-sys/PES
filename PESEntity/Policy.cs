using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PESEntity
{
    public class Policy
    {
        public string PolicyId { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
        public string PremiumPayment { get; set; }
        public string FilePath { get; set; }
        public Customer C { get; set; }
        public InsuranceProducts P { get; set; }
    }
}

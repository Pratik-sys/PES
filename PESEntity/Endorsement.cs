using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PESEntity
{
    public class Endorsement : Policy
    {
        public string EndorsementId { get; set; }
        public string FieldChanges { get; set; }
        public string EndorsementStatus { get; set; }
    }
}

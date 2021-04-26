using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PESEntity
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public bool Smoker { get; set; }
        public string Hobbies { get; set; }
    }
}

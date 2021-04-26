using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PESBusinessLayer;
using PESEntity;
using PESExceptions;

namespace PESConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Endorsement endorsement = new Endorsement() 
                {
                    EndorsementId = "EI00001",
                    PolicyId = "PN00001"
                };

                bool pa = EndorsementBL.AddEndorsemetBL(endorsement, 0);
                Console.WriteLine(pa);
            }
            catch (PolicyExceptions ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}

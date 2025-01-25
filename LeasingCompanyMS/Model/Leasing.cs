using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeasingCompanyMS.Model
{
    public class Leasing
    {
        public String User {  get; set; }
        public String Car { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }    
        public String Conditions { get; set; }  //todo requirements
        public Leasing() { 
        }
    }
}

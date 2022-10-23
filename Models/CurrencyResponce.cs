using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrencyResponce
    {       
        public int error { get; set; }
        public string message { get; set; }
        public decimal amount { get; set; }        
    }
}

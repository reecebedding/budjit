using System;
using System.Collections.Generic;
using System.Text;

namespace budjit.core.models
{
    public class Transaction
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public String Merchant { get; set; }
        public String Description { get; set; }
        public decimal Alteration { get; set; }
        public decimal Balance { get; set; }
    }
}

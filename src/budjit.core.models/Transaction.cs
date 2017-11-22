using System;

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

        public int? TagID { get; set; }
        public Tag Tag { get; set; }
    }
}

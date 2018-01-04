using System;
using System.ComponentModel.DataAnnotations;
using budjit.core.models;

namespace budjit.ui.API.ViewModel
{
    public class TransactionViewModel
    {
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public String Merchant { get; set; }
        public String Description { get; set; }
        [Required]
        public decimal Alteration { get; set; }
        public decimal Balance { get; set; }

        public int? TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
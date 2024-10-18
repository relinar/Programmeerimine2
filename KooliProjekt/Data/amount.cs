using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class invoice
    {
        [Required]
        [StringLength(25)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }
    }

    public class Amount
    {
        public int AmountID { get; set; }

       
        public int NutrientsID { get; set; }

        public DateTime AmountDate { get; set; }
    }
}

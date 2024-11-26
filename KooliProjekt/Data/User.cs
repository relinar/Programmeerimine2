using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Invoice
    {
        [Required]
        [StringLength(25)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }
    }

    public class User : Entity
    {
        public string Name { get; set; }
        public string Role { get; set; }

        public DateTime DailySummary { get; set; }
        public DateTime Meal { get; set; }
    }
}

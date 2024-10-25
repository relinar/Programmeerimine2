using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class INvoice
    {
        [Required]
        [StringLength(25)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }
    }

    public class Nutrients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Carbohydrates { get; set; }
        public float Sugars { get; set; }
        public float Fats { get; set; }

        public DateTime FoodChart { get; set; }
    }
}

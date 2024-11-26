using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class FoodChart : Entity
    {
        [Required]
        [StringLength(25)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }
        public string user { get; set; }
        public String date { get; set; }

        public string meal { get; set; }
        public DateTime nutrients { get; set; }
        public float amount { get; set; }

    }
}

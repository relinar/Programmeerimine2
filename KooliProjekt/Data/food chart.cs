using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class food_chart
    {
        [Required]
        [StringLength(25)]
        public string InvoiceNo { get; set; }

        [Required]
        public DateTime? InvoiceDate { get; set; }
        public int food_chartID { get; set; }
        public string user { get; set; }
        public String date { get; set; }

        public string meal { get; set; }
        public DateTime nutrients  { get; set; }
        public float amount { get; set; }

    }
}

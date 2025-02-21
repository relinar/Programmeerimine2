using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
 
    public class Amount
    {
        public int AmountID { get; set; }

       
        public int NutrientsID { get; set; }

        public DateTime AmountDate { get; set; }
        public string AmountTitle { get; set; }
    }
}

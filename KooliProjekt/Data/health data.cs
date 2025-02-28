namespace KooliProjekt.Data
{
    public class HealthData
    {
        public int Id { get; set; }  // Primaarvõti
        public int UserId { get; set; }  // Viide kasutajale (võõrvõti)
        public string User { get; set; } // Kasutaja nimi
        public string Date { get; set; } // Kuupäev või muu määratlus (näiteks mõõtmise kuupäev)

        public float BloodSugar { get; set; }  // Veresuhkur
        public float Weight { get; set; }      // Kaal
        public string BloodAir { get; set; }   // Vere hapnikusisaldus
        public float Systolic { get; set; }    // Süstoolne vererõhk
        public float Diastolic { get; set; }   // Diastoolne vererõhk
        public string Pulse { get; set; }      // Pulss

        // Täiendavad andmed
        public int Height { get; set; }         // Kõrgus
        public int Age { get; set; }            // Vanus
        public string Title { get; set; }
    }
}

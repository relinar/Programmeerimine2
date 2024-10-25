namespace KooliProjekt.Data
{
    public class HealthData
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Date { get; set; }

        public float BloodSugar { get; set; }
        public float Weight { get; set; }

        public string BloodAir { get; set; }

        public float Systolic { get; set; }
        public float Diastolic { get; set; }

        public string Pulse { get; set; }
    }
}

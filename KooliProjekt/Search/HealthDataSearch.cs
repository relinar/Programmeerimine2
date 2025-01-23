// File: Models/HealthDataSearch.cs
namespace KooliProjekt.Search
{
    public class HealthDataSearch
    {
        public string User { get; set; } // Filter by user name
        public float? BloodSugar { get; set; } // Filter by blood sugar level (nullable)
        public float? Weight { get; set; } // Filter by weight (nullable)
        public string BloodAir { get; set; } // Filter by blood air content
        public float? Systolic { get; set; } // Filter by systolic pressure
        public float? Diastolic { get; set; } // Filter by diastolic pressure
        public string Pulse { get; set; } // Filter by pulse rate
        public int? Age { get; set; } // Filter by age (nullable)
    }
}

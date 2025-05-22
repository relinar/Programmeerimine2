namespace KooliProjekt.PublicAPI
{
    public class Result
    {
        public string? ErrorMessage { get; set; }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }
    }
}
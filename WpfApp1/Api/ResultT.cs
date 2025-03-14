namespace WpfApp1.Api
{
    public class Result<T> : Result
    {
        public T Value { get; set; }
    }
}

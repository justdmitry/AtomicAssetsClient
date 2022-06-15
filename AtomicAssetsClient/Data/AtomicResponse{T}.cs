namespace AtomicAssetsClient.Data
{
    public class AtomicResponse<T>
    {
        public bool? Success { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}

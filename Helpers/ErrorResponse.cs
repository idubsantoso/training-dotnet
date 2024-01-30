namespace WebApi.Helpers
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public required string StatusPhrase { get; set; }
        public List<string> Errors { get; } = new();
        public DateTime? Timestamp { get; set; }

    }
}

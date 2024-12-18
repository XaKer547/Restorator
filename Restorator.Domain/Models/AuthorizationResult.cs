namespace Restorator.Domain.Models
{
    public class AuthorizationResult
    {
        public bool Success => Error is null && SessionInfo != null;
        public SessionInfo? SessionInfo { get; set; }
        public string? Error { get; set; }
    }
}
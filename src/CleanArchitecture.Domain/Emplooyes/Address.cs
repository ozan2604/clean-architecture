namespace CleanArchitecture.Domain.Emplooyes
{
    public sealed record Address
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? FulLAddress { get; set; }
    }

}

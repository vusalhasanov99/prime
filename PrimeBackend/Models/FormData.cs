namespace PrimeBackend.Models
{
    public class FormData
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Occupation { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}

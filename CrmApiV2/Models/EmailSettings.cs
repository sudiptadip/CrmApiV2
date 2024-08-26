namespace CrmApiV2.Models
{
    public class EmailSettings
    {
        public string EmailHost { get; set; }
        public int EmailPort { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public bool EnableSSL { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
    }
}

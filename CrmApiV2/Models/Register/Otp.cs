using System.ComponentModel.DataAnnotations.Schema;

namespace CrmApiV2.Models.Register
{
    public class Otp
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; }
    }
}

namespace CrmApiV2.Models
{
    public class UserTimeLog
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public TimeSpan? BreakTime { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}

namespace CrmApiV2.Models
{
    public class DailyUserSummary
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TotalWorkingTime { get; set; }
        public TimeSpan TotalBreakTime { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}

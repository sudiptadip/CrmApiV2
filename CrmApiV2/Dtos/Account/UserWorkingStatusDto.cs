namespace CrmApiV2.Dtos.Account
{
    public class UserWorkingStatusDto
    {
        public DateTime Date { get; set; }
        public DateTime? FirstLogin { get; set; }
        public DateTime? LastLogin { get; set; }
        public TimeSpan TotalBreakTime { get; set; }
        public TimeSpan TotalWorkingTime { get; set; }
        public string Status { get; set; }
    }
}

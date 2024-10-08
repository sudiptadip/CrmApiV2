﻿using CrmApiV2.Models.Register;

namespace CrmApiV2.Models
{
    public class DailyUserSummary
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime Date { get; set; }

        public long TotalWorkingTimeInSeconds { get; set; }
        public long TotalBreakTimeInSeconds { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public TimeSpan TotalWorkingTime => TimeSpan.FromSeconds(TotalWorkingTimeInSeconds);
        public TimeSpan TotalBreakTime => TimeSpan.FromSeconds(TotalBreakTimeInSeconds);
    }
}

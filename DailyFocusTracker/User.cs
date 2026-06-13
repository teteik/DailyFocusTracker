namespace DailyFocusTracker;

public record UserProfile
{
    public required string Username { get; init; }
    public int Streak { get; init; }
    public int MaxStreak { get; init; }
    public int SessionCount {  get; init; }
    public int TotalMinutes { get; init; }
    public int TodayTotalMinutes { get; init; }
    public DateTime LastSessionDate { get; init; }

    
    public UserProfile AddSession(int minutes)
    {
        if (minutes <= 0)
                throw new ArgumentException("Session duration must be greater than 0.", nameof(minutes));

        DateTime today = DateTime.Today;
        DateTime sessionDate = this.LastSessionDate.Date; 

        int daysDifference = (today - sessionDate).Days;

        int newStreak = daysDifference switch
        {
            0 => this.Streak,          
            1 => this.Streak + 1,      
            _ => 1                     
        };

        int todayMinutes = minutes;
        if (newStreak == this.Streak)
        {
            todayMinutes += this.TodayTotalMinutes;
        }

        return this with 
        {
            TotalMinutes = this.TotalMinutes + minutes,
            SessionCount = this.SessionCount + 1,
            Streak = newStreak,
            LastSessionDate = today,
            TodayTotalMinutes = todayMinutes,
            MaxStreak = Math.Max(this.MaxStreak, newStreak)
        };
    }
}
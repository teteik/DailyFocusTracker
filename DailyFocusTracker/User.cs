namespace DailyFocusTracker;

public record UserProfile
{
    public required string Username { get; init; }
    public int Streak { get; set; }
    public int MaxStreak { get; set; }
    public int SessionCount {  get; set; }
    public int TotalMinutes { get; init; }
    public DateTime LastSessionDate { get; set; }

    
    public UserProfile AddSession(int minutes)
    {
        if (minutes <= 0)
                throw new ArgumentException("Время сессии должно быть больше нуля.", nameof(minutes));

        DateTime today = DateTime.Today;
        DateTime sessionDate = this.LastSessionDate.Date; 

        int daysDifference = (today - sessionDate).Days;

        int newStreak = daysDifference switch
        {
            0 => this.Streak,          
            1 => this.Streak + 1,      
            _ => 1                     
        };

        return this with 
        {
            TotalMinutes = this.TotalMinutes + minutes,
            SessionCount = this.SessionCount + 1,
            Streak = newStreak,
            LastSessionDate = sessionDate,
            MaxStreak = Math.Max(this.MaxStreak, newStreak)
        };
    }
}
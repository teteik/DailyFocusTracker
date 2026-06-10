namespace DailyFocusTracker;

public record UserProfile
{
    public string Username { get; init; }
    public int Streak { get; set; }
    public int MaxStreak { get; set; }
    public int SessionCount {  get; set; }
    public int TotalMinutes { get; init; }
    public DateTime SessionLastTime { get; set; }

    
    public UserProfile AddSession(int minutes)
    {
        if (minutes <= 0)
                throw new ArgumentException("Время сессии должно быть больше нуля.", nameof(minutes));

        int newStreak;
        DateTime newSessionLastTime = DateTime.Today;
        TimeSpan difference = newSessionLastTime - SessionLastTime;
        if (difference.TotalDays > 1)
            newStreak = 1;
        if (difference.TotalDays == 0)
            newStreak = Streak;
        else
            newStreak = Streak + 1;

        return this with 
        {
            TotalMinutes = this.TotalMinutes + minutes,
            SessionCount = this.SessionCount + 1,
            Streak = newStreak,
            SessionLastTime = newSessionLastTime,
            MaxStreak = Math.Max(this.MaxStreak, newStreak)
        };
    }
}
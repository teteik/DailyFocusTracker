namespace DailyFocusTracker;

public class Program
{
    private static int Goal = 60;
    public static void Main(string[] args)
    {
        var storage = new StorageService("profile.json");
        
        var userProfile = storage.LoadUserProfile();
        
       userProfile = Login(userProfile);
        
        Console.WriteLine("Enter session time in minutes: ");
        int sessionMinutes;
        while (!int.TryParse(Console.ReadLine(), out sessionMinutes) || sessionMinutes <= 0)
        {
            Console.WriteLine("Error. Enter a positive number: ");
        }
        
        userProfile = userProfile.AddSession(sessionMinutes);
        
        storage.SaveUserProfile(userProfile);
        
        PrintStats(userProfile);
    }

    private static UserProfile Login(UserProfile? userProfile)
    {
        if (userProfile == null)
        {
            Console.WriteLine("Enter username: ");
            string username = Console.ReadLine()?.Trim() ?? "Unknown";
            
            userProfile = new UserProfile
            {
                Username = username,
                Streak = 0,
                MaxStreak = 0,
                SessionCount = 0,
                TotalMinutes = 0,
                LastSessionDate = DateTime.MinValue
            };
            Console.WriteLine($"Hello {userProfile.Username}! New profile created.");
        }
        else
        {
            Console.WriteLine($"Hello {userProfile.Username}! Welcome back.");
            Console.WriteLine($"Total time: {userProfile.TotalMinutes / 60} hours {userProfile.TotalMinutes % 60} minutes.");
            Console.WriteLine($"Current streak: {userProfile.Streak}");
        }
        
        return userProfile;
    }

    private static void PrintStats(UserProfile userProfile)
    {
        Console.WriteLine("Session time saved!");
        Console.WriteLine($"New streak: {userProfile.Streak}");
        Console.WriteLine($"Total minutes: {userProfile.TotalMinutes}");
        var avgSessionTime = userProfile.SessionCount > 0 ? (userProfile.TotalMinutes / userProfile.SessionCount) : 0;
        Console.WriteLine($"Average session time: {avgSessionTime}");
        
        var filled = (int) Math.Round(userProfile.TodayTotalMinutes / (double) Goal * Goal);
        string bar = new string('|', filled) + new string('-', Goal - filled);
        Console.WriteLine($"Daily progress: {bar} ({filled} / {Goal}) {filled/(double) Goal * 100 :F1}%");
    }
}
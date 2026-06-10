namespace DailyFocusTracker;

public class Program
{
    public static void Main(string[] args)
    {
        var storage = new StorageService("profile.json");
        
        var userProfile = storage.LoadUserProfile();
        
        if (userProfile == null)
        {
            Console.Write("Enter username: \n");
            string username = Console.ReadLine()?.Trim() ?? "Unknown";
            
            userProfile = new UserProfile
            {
                Username = username,
                Streak = 0,
                MaxStreak = 0,
                SessionCount = 0,
                TotalMinutes = 0,
                SessionLastTime = DateTime.MinValue
            };
            Console.WriteLine($"Hello {userProfile.Username}! New profile created.");
        }
        else
        {
            Console.WriteLine($"Hello {userProfile.Username}! Welcome back.");
        }
        
        Console.Write("Enter session time in minutes: ");
        int sessionMinutes;
        while (!int.TryParse(Console.ReadLine(), out sessionMinutes) || sessionMinutes <= 0)
        {
            Console.Write("Error. Enter a positive number: ");
        }
        
        userProfile = userProfile.AddSession(sessionMinutes);
        
        storage.SaveUserProfile(userProfile);
        
        Console.WriteLine("\nSession time saved!");
        Console.WriteLine($"New streak: {userProfile.Streak}");
        Console.WriteLine($"Total minutes: {userProfile.TotalMinutes}");
    }    
}
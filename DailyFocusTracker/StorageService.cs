using System.Text.Json;

namespace DailyFocusTracker;

public class StorageService
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options;
    
    public StorageService(string filePath)
    {
        _filePath = filePath;
        _options = new JsonSerializerOptions { WriteIndented = true };
    }

    public UserProfile? LoadUserProfile()
    {
        if (!File.Exists(_filePath))
        {
            return null; 
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<UserProfile>(json, _options);
        }
        catch (JsonException e)
        {
            Console.WriteLine($"[WARN] Failed to load user profile: {e.Message}");
            return null;
        }
    }

    public void SaveUserProfile(UserProfile userProfile)
    {
        string json = JsonSerializer.Serialize(userProfile, _options);
        File.WriteAllText(_filePath, json);
    }
}
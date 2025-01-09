namespace SpotifyBetterRandom.Helpers;

public static class ObjectExtensions
{
    public static void PrintMembers<T>(this T obj)
    {
        foreach (var prop in obj.GetType().GetProperties())
        {
            Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
        }
    }
}
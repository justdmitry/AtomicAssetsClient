using System.Text.Json;

namespace AtomicAssetsClient.Utils
{
    /// <summary>
    /// Based on https://stackoverflow.com/questions/58570189/
    /// </summary>
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLowerInvariant();
        }
    }
}

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AtomicAssetsClient.Utils
{
    public class TimeJsonConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var val = reader.GetString();
            var milliseconds = long.Parse(val, CultureInfo.InvariantCulture);
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
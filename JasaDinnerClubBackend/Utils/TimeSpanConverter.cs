using System.Text.Json;
using System.Text.Json.Serialization;

namespace JasaDinnerClubBackend.Utils // Replace ProjectName with your project's namespace
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse TimeSpan from a string in "HH:mm:ss" format
            var value = reader.GetString();
            if (TimeSpan.TryParse(value, out var timeSpan))
            {
                return timeSpan;
            }

            throw new JsonException("Invalid TimeSpan format. Expected 'HH:mm:ss'.");
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            // Write TimeSpan as a string in "HH:mm:ss" format
            writer.WriteStringValue(value.ToString(@"hh\:mm\:ss"));
        }
    }
}

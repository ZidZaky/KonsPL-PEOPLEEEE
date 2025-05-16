using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UTS_PEOPLEEEE
{
    public class JamOperationalConfig
    {
        [JsonPropertyName("modes")]
        public Dictionary<string, Mode> Modes { get; set; }

        [JsonPropertyName("default_mode")]
        public string DefaultMode { get; set; }

        public class Mode
        {
            [JsonPropertyName("open_hour")]
            public int OpenHour { get; set; }

            [JsonPropertyName("close_hour")]
            public int CloseHour { get; set; }
        }

        public static JamOperationalConfig Load(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<JamOperationalConfig>(json);
        }
    }
}

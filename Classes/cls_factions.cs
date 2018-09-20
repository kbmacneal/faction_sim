// Generated by https://quicktype.io

namespace faction_sim.Classes.Fations
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Faction
    {
        [JsonProperty("Faction Name")]
        public string FactionName { get; set; }

        [JsonProperty("Force")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Force { get; set; }

        [JsonProperty("Cunning")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Cunning { get; set; }

        [JsonProperty("Wealth")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Wealth { get; set; }
    }

    public partial class Faction
    {
        public static Faction[] FromJson(string json) => JsonConvert.DeserializeObject<Faction[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Faction[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}

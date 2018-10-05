// Generated by https://quicktype.io

namespace faction_sim.Classes.Assets
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Asset
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("HP")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Hp { get; set; }

        [JsonProperty("Attack")]
        public string Attack { get; set; }

        [JsonProperty("Counterattack")]
        public string Counterattack { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("AttackStats")]
        public string AttackStats { get; set; }

        [JsonProperty("AttackDice")]
        public string AttackDice { get; set; }

        [JsonProperty("DefenderReroll")]
        public bool DefenderReroll { get; set; }

        [JsonProperty("AttackerReroll")]
        public bool AttackerReroll { get; set; }

        [JsonProperty("AttackerExtraDice")]
        public bool AttackerExtraDice { get; set; }

        [JsonProperty("DefenderExtraDice")]
        public bool DefenderExtraDice { get; set; }
    }

    public enum AttackStats { CvC, CvW, FvC, FvF, FvW, None, WvF, WvW };

    public partial class Asset
    {
        public static Asset[] FromJson(string json) => JsonConvert.DeserializeObject<Asset[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Asset[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                AttackStatsConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AttackStatsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AttackStats) || t == typeof(AttackStats?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CvC":
                    return AttackStats.CvC;
                case "CvW":
                    return AttackStats.CvW;
                case "FvC":
                    return AttackStats.FvC;
                case "FvF":
                    return AttackStats.FvF;
                case "FvW":
                    return AttackStats.FvW;
                case "None":
                    return AttackStats.None;
                case "WvF":
                    return AttackStats.WvF;
                case "WvW":
                    return AttackStats.WvW;
            }
            throw new Exception("Cannot unmarshal type AttackStats");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AttackStats)untypedValue;
            switch (value)
            {
                case AttackStats.CvC:
                    serializer.Serialize(writer, "CvC");
                    return;
                case AttackStats.CvW:
                    serializer.Serialize(writer, "CvW");
                    return;
                case AttackStats.FvC:
                    serializer.Serialize(writer, "FvC");
                    return;
                case AttackStats.FvF:
                    serializer.Serialize(writer, "FvF");
                    return;
                case AttackStats.FvW:
                    serializer.Serialize(writer, "FvW");
                    return;
                case AttackStats.None:
                    serializer.Serialize(writer, "None");
                    return;
                case AttackStats.WvF:
                    serializer.Serialize(writer, "WvF");
                    return;
                case AttackStats.WvW:
                    serializer.Serialize(writer, "WvW");
                    return;
            }
            throw new Exception("Cannot marshal type AttackStats");
        }

        public static readonly AttackStatsConverter Singleton = new AttackStatsConverter();
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

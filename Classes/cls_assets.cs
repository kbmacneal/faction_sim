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
        public long Hp { get; set; }

        [JsonProperty("Cost")]
        public string Cost { get; set; }

        [JsonProperty("TL")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Tl { get; set; }

        [JsonProperty("Type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("Attack")]
        public string Attack { get; set; }

        [JsonProperty("Counterattack")]
        public string Counterattack { get; set; }

        [JsonProperty("Special")]
        public Special Special { get; set; }

        [JsonProperty("Upkeep")]
        public string Upkeep { get; set; }

        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("AttackStats")]
        public string AttackStats { get; set; }

        [JsonProperty("AttackDice")]
        public string AttackDice { get; set; }
        [JsonProperty("AttackedAlready")]
        public bool AttackedAlready { get; set; } = false;
    }

    public enum Special { A, AS, ASP, Empty, P, S };

    public enum TypeEnum { Facility, LogisticsFacility, MilitaryUnit, Spaceship, Special, SpecialForces, Starship, Tactic };

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
                SpecialConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SpecialConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Special) || t == typeof(Special?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return Special.Empty;
                case "A":
                    return Special.A;
                case "A, S":
                    return Special.AS;
                case "A, S, P":
                    return Special.ASP;
                case "P":
                    return Special.P;
                case "S":
                    return Special.S;
            }
            throw new Exception("Cannot unmarshal type Special");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Special)untypedValue;
            switch (value)
            {
                case Special.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case Special.A:
                    serializer.Serialize(writer, "A");
                    return;
                case Special.AS:
                    serializer.Serialize(writer, "A, S");
                    return;
                case Special.ASP:
                    serializer.Serialize(writer, "A, S, P");
                    return;
                case Special.P:
                    serializer.Serialize(writer, "P");
                    return;
                case Special.S:
                    serializer.Serialize(writer, "S");
                    return;
            }
            throw new Exception("Cannot marshal type Special");
        }

        public static readonly SpecialConverter Singleton = new SpecialConverter();
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

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Facility":
                    return TypeEnum.Facility;
                case "Logistics Facility":
                    return TypeEnum.LogisticsFacility;
                case "Military Unit":
                    return TypeEnum.MilitaryUnit;
                case "Spaceship":
                    return TypeEnum.Spaceship;
                case "Special":
                    return TypeEnum.Special;
                case "Special Forces":
                    return TypeEnum.SpecialForces;
                case "Starship":
                    return TypeEnum.Starship;
                case "Tactic":
                    return TypeEnum.Tactic;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.Facility:
                    serializer.Serialize(writer, "Facility");
                    return;
                case TypeEnum.LogisticsFacility:
                    serializer.Serialize(writer, "Logistics Facility");
                    return;
                case TypeEnum.MilitaryUnit:
                    serializer.Serialize(writer, "Military Unit");
                    return;
                case TypeEnum.Spaceship:
                    serializer.Serialize(writer, "Spaceship");
                    return;
                case TypeEnum.Special:
                    serializer.Serialize(writer, "Special");
                    return;
                case TypeEnum.SpecialForces:
                    serializer.Serialize(writer, "Special Forces");
                    return;
                case TypeEnum.Starship:
                    serializer.Serialize(writer, "Starship");
                    return;
                case TypeEnum.Tactic:
                    serializer.Serialize(writer, "Tactic");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}

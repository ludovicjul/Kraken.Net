﻿using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Kraken.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kraken.Net.Converters
{
    internal class ExtendedDictionaryConverter<T>: JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var data = (KrakenDictionaryResult<T>) value!;
            writer.WriteStartObject();
            writer.WritePropertyName("data");
            writer.WriteRawValue(JsonConvert.SerializeObject(data.Data));
            writer.WritePropertyName("last");
            writer.WriteValue(JsonConvert.SerializeObject(data.Last, new TimestampSecondsConverter()));
            writer.WriteEndObject();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var inner = obj.First;
            if (inner == default || inner.First == default)
                return null;

            var data = inner.First.ToObject<T>();
            var result = (KrakenDictionaryResult<T>)Activator.CreateInstance(objectType);
            result.Data = data!;
            var lastValue = obj["last"];
            if (lastValue != null)
            {
                var timestamp = lastValue.Value<long>();
                if (timestamp > 1000000000000000000)
                    result.Last = lastValue.ToObject<DateTime>(new JsonSerializer() { Converters = { new TimestampNanoSecondsConverter() } });
                else
                    result.Last = lastValue.ToObject<DateTime>(new JsonSerializer() { Converters = { new TimestampSecondsConverter() } });
            }
            return Convert.ChangeType(result, objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }

    /// <summary>
    /// Dictionary result
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    public class KrakenDictionaryResult<T>
    {
        /// <summary>
        /// The data
        /// </summary>
        public T Data { get; set; } = default!;
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonConverter(typeof(TimestampSecondsConverter))]
        public DateTime Last { get; set; }
    }

    /// <summary>
    /// Kline result
    /// </summary>
    [JsonConverter(typeof(ExtendedDictionaryConverter<IEnumerable<KrakenKline>>))]
    public class KrakenKlinesResult : KrakenDictionaryResult<IEnumerable<KrakenKline>>
    {
    }

    /// <summary>
    /// Trade result
    /// </summary>
    [JsonConverter(typeof(ExtendedDictionaryConverter<IEnumerable<KrakenTrade>>))]
    public class KrakenTradesResult : KrakenDictionaryResult<IEnumerable<KrakenTrade>>
    {
    }

    /// <summary>
    /// Spread result
    /// </summary>
    [JsonConverter(typeof(ExtendedDictionaryConverter<IEnumerable<KrakenSpread>>))]
    public class KrakenSpreadsResult : KrakenDictionaryResult<IEnumerable<KrakenSpread>>
    {
    }
}

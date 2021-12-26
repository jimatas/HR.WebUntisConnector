// Copyright (c) 2019-2021 Jim Atas, Rotterdam University of Applied Sciences. All rights reserved.
// This source file is part of WebUntisConnector, which is proprietary software of Rotterdam University of Applied Sciences.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HR.WebUntisConnector.JsonRpc.Infrastructure
{
    internal class ObjectJsonConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var tokenType = reader.TokenType;
            if (tokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (tokenType == JsonTokenType.Number)
            {
                if (reader.TryGetInt32(out var int32Value))
                {
                    return int32Value;
                }

                if (reader.TryGetInt64(out var int64Value))
                {
                    return int64Value;
                }

                if (reader.TryGetDouble(out var doubleValue))
                {
                    return doubleValue;
                }
            }

            if (tokenType == JsonTokenType.True || tokenType == JsonTokenType.False)
            {
                return reader.GetBoolean();
            }

            if (tokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                return jsonDocument.RootElement.Clone();
            }
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
            => throw new NotSupportedException("Directly writing System.Object is not supported.");
    }
}

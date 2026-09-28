using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AdvancedFeatures.Reflection
{
    /// <summary>
    /// Dynamic JSON: the payload shape changes — you only know the property <b>name</b> or a <b>value</b> at runtime.
    /// <para>
    /// Use <see cref="JsonNode"/> / <see cref="JsonElement"/> for get-by-name and get-object-by-property-value.
    /// Contrast <see cref="ClrGetPropertyOnJson"/> — CLR reflection looks for .NET members, not JSON keys.
    /// </para>
    /// </summary>
    public static class DynamicJsonLookup
    {
        /// <summary>
        /// For a JSON <b>array of objects</b>, read the same property name from each item.
        /// Missing keys become null — shapes can differ per element.
        /// </summary>
        public static IReadOnlyList<string?> GetByPropertyNameFromEach(string jsonArray, string propertyName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(jsonArray);
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

            var root = RequireArray(jsonArray);
            var values = new List<string?>();
            foreach (var item in root)
            {
                if (item is not JsonObject obj)
                {
                    values.Add(null);
                    continue;
                }

                values.Add(FormatNode(obj, propertyName));
            }

            return values;
        }

        /// <summary>
        /// Parse a JSON object and read a property by name.
        /// </summary>
        public static string? GetByPropertyName(string json, string propertyName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(json);
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

            return FormatNode(RequireObject(json), propertyName);
        }

        /// <summary>
        /// Lower-level: <see cref="JsonElement.TryGetProperty"/>.
        /// </summary>
        public static string? GetWithJsonElement(string json, string propertyName)
        {
            using var document = JsonDocument.Parse(json);
            if (!document.RootElement.TryGetProperty(propertyName, out var property))
                return null;

            return FormatElement(property);
        }

        /// <summary>
        /// In a JSON <b>array of objects</b>, return the first object where <paramref name="propertyName"/> equals <paramref name="value"/>.
        /// Result is compact JSON text, or null if none match.
        /// </summary>
        public static string? GetObjectByPropertyValue(string jsonArray, string propertyName, string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(jsonArray);
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
            ArgumentNullException.ThrowIfNull(value);

            var root = RequireArray(jsonArray);

            foreach (var item in root)
            {
                if (item is not JsonObject obj)
                    continue;

                var actual = FormatNode(obj, propertyName);
                if (string.Equals(actual, value, StringComparison.Ordinal))
                    return obj.ToJsonString();
            }

            return null;
        }

        /// <summary>
        /// Prints why <c>Deserialize&lt;object&gt;</c> + CLR <c>GetProperty</c> fails on JSON keys.
        /// Use <see cref="GetByPropertyName"/> instead.
        /// </summary>
        public static void ClrGetPropertyOnJson(string json, string propertyName)
        {
            Console.WriteLine("Wrong tool for JSON keys (CLR GetProperty):");
            Console.WriteLine($"  1) JSON text contains key \"{propertyName}\".");

            var deserialized = JsonSerializer.Deserialize<object>(json);
            var runtimeType = deserialized?.GetType().Name ?? "(null)";
            Console.WriteLine($"  2) Deserialize<object> runtime type = {runtimeType} (not a class with your JSON fields).");

            var property = deserialized?.GetType().GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);

            Console.WriteLine(
                property is null
                    ? $"  3) GetProperty(\"{propertyName}\") → null (no CLR member with that name)."
                    : $"  3) GetProperty(\"{propertyName}\") found a CLR member (unexpected for typical JSON object).");

            Console.WriteLine("  4) Use JsonNode / JsonElement APIs to read JSON keys, not CLR properties.");
        }

        private static JsonObject RequireObject(string json) =>
            JsonNode.Parse(json) as JsonObject
            ?? throw new InvalidOperationException("Expected a JSON object at the root.");

        private static JsonArray RequireArray(string json) =>
            JsonNode.Parse(json) as JsonArray
            ?? throw new InvalidOperationException("Expected a JSON array at the root.");

        private static string? FormatNode(JsonObject root, string propertyName)
        {
            if (!root.TryGetPropertyValue(propertyName, out var node) || node is null)
                return null;

            return NodeToSearchText(node);
        }

        private static string NodeToSearchText(JsonNode node) =>
            node is JsonValue
                ? node.ToString()
                : node.ToJsonString();

        private static string? FormatElement(JsonElement property) =>
            property.ValueKind switch
            {
                JsonValueKind.String => property.GetString(),
                JsonValueKind.Number => property.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Null => null,
                _ => property.GetRawText()
            };
    }
}

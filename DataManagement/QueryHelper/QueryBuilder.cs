using System.Data;
using System.Data.Common;

namespace DataManagement.Queries
{
    /// <summary>
    /// Loads SQL from files and binds parameters without string concatenation/replacement.
    /// </summary>
    public static class QueryBuilder
    {
        public static BuiltQuery BuildQuery(string queryName, IEnumerable<Param>? queryParams = null, string? path = null)
        {
            var filePath = path == null
                ? Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "QueryHelper", "Queries", $"{queryName}.sql"))
                : Path.Combine(path, $"{queryName}.sql");

            var sql = File.ReadAllText(filePath);
            var parameters = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);

            if (queryParams != null)
            {
                foreach (var param in queryParams.Where(p => p != null))
                {
                    if (string.IsNullOrWhiteSpace(param!.Name))
                    {
                        continue;
                    }

                    // Keep the placeholder in SQL (e.g. @nameParam); bind the value separately.
                    parameters[param.Name] = ConvertValue(param);
                }
            }

            return new BuiltQuery(sql, parameters);
        }

        public static DbCommand CreateCommand(DbConnection connection, BuiltQuery query)
        {
            var command = connection.CreateCommand();
            command.CommandText = query.Sql;
            command.CommandType = CommandType.Text;

            foreach (var pair in query.Parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = pair.Key.StartsWith("@", StringComparison.Ordinal)
                    ? pair.Key
                    : "@" + pair.Key;
                parameter.Value = pair.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private static object? ConvertValue(Param param)
        {
            if (param.Value == null)
            {
                return null;
            }

            return param.ParamType switch
            {
                ParamType.Int => int.Parse(param.Value),
                ParamType.Double => double.Parse(param.Value),
                ParamType.String => param.Value,
                _ => param.Value
            };
        }
    }

    public sealed class BuiltQuery
    {
        public BuiltQuery(string sql, IReadOnlyDictionary<string, object?> parameters)
        {
            Sql = sql;
            Parameters = parameters;
        }

        public string Sql { get; }
        public IReadOnlyDictionary<string, object?> Parameters { get; }
    }

    public class Param
    {
        public string Name { get; set; } = string.Empty;
        public string? Value { get; set; }
        public ParamType ParamType { get; set; }
    }

    public enum ParamType
    {
        Int = 1,
        String = 2,
        Double = 3
    }

    public class Query
    {
        public string Name { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public Dictionary<string, string> Params { get; set; } = new();
    }
}

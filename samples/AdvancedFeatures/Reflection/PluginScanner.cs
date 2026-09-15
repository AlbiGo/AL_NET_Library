using System.Reflection;

namespace AdvancedFeatures.Reflection
{
    /// <summary>
    /// Prefer reflection — discover and activate types that opted in with <see cref="PluginAttribute"/>.
    /// <para>
    /// This is the teaching “work” for reflection (like <c>CarServices</c> for delegates):
    /// scan once, filter by attribute + <see cref="IPlugin"/>, <see cref="Activator.CreateInstance"/>,
    /// then call through the interface. Main never lists <see cref="HelloPlugin"/> by name.
    /// </para>
    /// <para>
    /// Avoid using reflection for everyday property access when you already know the type —
    /// prefer normal C# members (faster, safer, refactor-friendly). Cache <see cref="Type"/> /
    /// <see cref="PropertyInfo"/> if you must reflect in a hot path.
    /// </para>
    /// </summary>
    public static class PluginScanner
    {
        /// <summary>
        /// Finds all concrete <see cref="IPlugin"/> types in <paramref name="assembly"/>
        /// that carry <see cref="PluginAttribute"/>, creates instances, returns them.
        /// </summary>
        public static IReadOnlyList<IPlugin> Discover(Assembly assembly)
        {
            ArgumentNullException.ThrowIfNull(assembly);

            var plugins = new List<IPlugin>();

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (!typeof(IPlugin).IsAssignableFrom(type))
                    continue;

                var marker = type.GetCustomAttribute<PluginAttribute>();
                if (marker is null)
                    continue;

                // Prefer: Activator when the type is only known at runtime (plugin / extension).
                var instance = Activator.CreateInstance(type) as IPlugin
                    ?? throw new InvalidOperationException($"Could not create plugin {type.Name}.");

                Console.WriteLine($"Discovered plugin '{marker.Name}' → {type.Name}");
                plugins.Add(instance);
            }

            return plugins;
        }
    }
}

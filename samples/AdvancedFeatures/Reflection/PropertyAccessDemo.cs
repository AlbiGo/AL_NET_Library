using System.Reflection;

namespace AdvancedFeatures.Reflection
{
    /// <summary>
    /// Tiny model used to contrast Prefer (normal members) vs Avoid (uncached reflection).
    /// </summary>
    public sealed class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }

    /// <summary>
    /// Prefer / Avoid contrast for reading a property.
    /// <para>
    /// Prefer: <c>person.Name</c> when the type is known at compile time.
    /// Prefer (if you must reflect): cache <see cref="PropertyInfo"/> once.
    /// Avoid: <c>GetProperty("Name")</c> inside a tight loop with a magic string every time.
    /// </para>
    /// </summary>
    public static class PropertyAccessDemo
    {
        // Cached once — Prefer when reflection is required repeatedly.
        private static readonly PropertyInfo NameProperty =
            typeof(Person).GetProperty(nameof(Person.Name))
            ?? throw new InvalidOperationException("Person.Name not found.");

        public static string PreferDirect(Person person) => person.Name;

        public static string PreferCachedReflection(Person person)
            => (string?)NameProperty.GetValue(person) ?? "";

        /// <summary>
        /// Avoid pattern (shown for teaching): look up the property by string on every call.
        /// Slow and brittle under rename — use <see cref="nameof"/> at least, and cache if repeated.
        /// </summary>
        public static string AvoidUncachedMagicString(Person person)
        {
            var property = typeof(Person).GetProperty("Name"); // magic string, no cache
            return (string?)property?.GetValue(person) ?? "";
        }
    }
}

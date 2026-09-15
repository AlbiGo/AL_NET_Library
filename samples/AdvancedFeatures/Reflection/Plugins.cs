namespace AdvancedFeatures.Reflection
{
    /// <summary>
    /// Marker attribute — Prefer reflection discovers types that opt in.
    /// <para>
    /// Without an attribute (or interface), assembly scanning becomes a grab-bag of every class.
    /// Plugins tag themselves; <see cref="PluginScanner"/> only activates marked types.
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PluginAttribute : Attribute
    {
        public string Name { get; }

        public PluginAttribute(string name) => Name = name;
    }

    /// <summary>Contract every discovered plugin must implement.</summary>
    public interface IPlugin
    {
        string Describe();
    }

    /// <summary>Sample plugin A — discovered via <see cref="PluginAttribute"/>, not hard-coded in Main.</summary>
    [Plugin("hello")]
    public sealed class HelloPlugin : IPlugin
    {
        public string Describe() => "HelloPlugin: greets the curriculum";
    }

    /// <summary>Sample plugin B — second opt-in type so the scanner finds more than one.</summary>
    [Plugin("echo")]
    public sealed class EchoPlugin : IPlugin
    {
        public string Describe() => "EchoPlugin: echoes that reflection found me";
    }
}

namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Subscriber A — reacts to task lifecycle (e.g. update app state / UI).
    /// <para>
    /// Method shape matches <c>EventHandler&lt;TaskEventArgs&gt;</c>:
    /// <c>(object? sender, TaskEventArgs e)</c>. Reads <see cref="TaskEventArgs.Title"/> —
    /// same raise as <see cref="EmailService"/>, different side effect.
    /// </para>
    /// </summary>
    public class AppService
    {
        public void OnTaskCreated(object? source, TaskEventArgs e)
            => Console.WriteLine($"AppService: created '{e.Title}'.");

        public void OnTaskCompleted(object? source, TaskEventArgs e)
            => Console.WriteLine($"AppService: completed '{e.Title}'.");
    }
}

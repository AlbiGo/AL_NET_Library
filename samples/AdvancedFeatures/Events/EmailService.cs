namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Subscriber B — same events, different side effect (send email).
    /// <para>
    /// Shows why events are useful: <see cref="TaskService"/> raises once;
    /// this type and <see cref="AppService"/> both run without the publisher knowing either by name.
    /// </para>
    /// </summary>
    public class EmailService
    {
        public void OnTaskCreated(object? source, TaskEventArgs e)
            => Console.WriteLine($"EmailService: mailed 'created: {e.Title}'.");

        public void OnTaskCompleted(object? source, TaskEventArgs e)
            => Console.WriteLine($"EmailService: mailed 'completed: {e.Title}'.");
    }
}

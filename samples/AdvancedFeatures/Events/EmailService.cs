namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Subscriber B: same events, different side effect (send email).
    /// Shows why events are useful — many listeners, one raise.
    /// </summary>
    public class EmailService
    {
        public void OnTaskCreated(object? source, TaskEventArgs e)
            => Console.WriteLine($"EmailService: mailed 'created: {e.Title}'.");

        public void OnTaskCompleted(object? source, TaskEventArgs e)
            => Console.WriteLine($"EmailService: mailed 'completed: {e.Title}'.");
    }
}

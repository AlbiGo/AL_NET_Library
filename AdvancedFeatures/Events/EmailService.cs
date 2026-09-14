namespace AdvancedFeatures.Events
{
    /// <summary>Subscriber: reacts to the same events with a different side effect (notify by email).</summary>
    public class EmailService
    {
        public void OnTaskCreated(object? source, EventArgs eventArgs)
            => Console.WriteLine("EmailService: sent 'task created'.");

        public void OnTaskCompleted(object? source, EventArgs eventArgs)
            => Console.WriteLine("EmailService: sent 'task completed'.");
    }
}

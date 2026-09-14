namespace AdvancedFeatures.Events
{
    /// <summary>Subscriber: reacts to task lifecycle events (e.g. update UI / app state).</summary>
    public class AppService
    {
        public void OnTaskCreated(object? source, EventArgs eventArgs)
            => Console.WriteLine("AppService: task created.");

        public void OnTaskCompleted(object? source, EventArgs eventArgs)
            => Console.WriteLine("AppService: task completed.");
    }
}

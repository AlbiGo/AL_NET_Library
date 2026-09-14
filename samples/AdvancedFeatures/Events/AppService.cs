namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Subscriber A: reacts to task lifecycle (e.g. update app state / UI).
    /// Reads Title from TaskEventArgs — same raise, useful payload.
    /// </summary>
    public class AppService
    {
        public void OnTaskCreated(object? source, TaskEventArgs e)
            => Console.WriteLine($"AppService: created '{e.Title}'.");

        public void OnTaskCompleted(object? source, TaskEventArgs e)
            => Console.WriteLine($"AppService: completed '{e.Title}'.");
    }
}

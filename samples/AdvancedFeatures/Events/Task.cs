namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Demo work item passed into <see cref="TaskService"/>.
    /// <para>
    /// Named <c>TaskItem</c> so it does not clash with <c>System.Threading.Tasks.Task</c>.
    /// Title is copied into <see cref="TaskEventArgs"/> when events are raised.
    /// </para>
    /// </summary>
    public class TaskItem
    {
        public string? Title { get; set; }
    }
}

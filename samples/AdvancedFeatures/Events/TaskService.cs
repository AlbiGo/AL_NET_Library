namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Custom EventArgs so subscribers receive the task title (not just “something happened”).
    /// Prefer <c>EventHandler&lt;TaskEventArgs&gt;</c> over empty <see cref="EventArgs"/> when payload matters.
    /// </summary>
    public class TaskEventArgs : EventArgs
    {
        public required string Title { get; init; }
    }

    /// <summary>
    /// Publisher for the events demo — owns the events and is the only type allowed to raise them.
    /// <para>
    /// <c>event EventHandler&lt;T&gt;?</c> → outsiders may only <c>+=</c> / <c>-=</c>.
    /// They cannot assign null or <c>Invoke</c> from outside (unlike a public delegate field).
    /// <see cref="AppService"/> and <see cref="EmailService"/> are parallel listeners:
    /// one raise, many side effects. Payload travels in <see cref="TaskEventArgs"/>.
    /// </para>
    /// </summary>
    public class TaskService
    {
        public event EventHandler<TaskEventArgs>? TaskCreated;
        public event EventHandler<TaskEventArgs>? TaskCompleted;

        public void PrepareTask(TaskItem work)
        {
            Console.WriteLine($"Preparing '{work.Title}'...");
            OnTaskCreated(new TaskEventArgs { Title = work.Title ?? "(untitled)" });
        }

        public void CompleteTask(TaskItem work)
        {
            Console.WriteLine($"Completed '{work.Title}'.");
            OnTaskCompleted(new TaskEventArgs { Title = work.Title ?? "(untitled)" });
        }

        /// <summary>Conventional protected raiser — subclasses can customize; null-safe if nobody subscribed.</summary>
        protected virtual void OnTaskCreated(TaskEventArgs e) => TaskCreated?.Invoke(this, e);

        protected virtual void OnTaskCompleted(TaskEventArgs e) => TaskCompleted?.Invoke(this, e);
    }
}

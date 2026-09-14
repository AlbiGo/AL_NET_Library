namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Custom EventArgs so subscribers receive the task title (not just "something happened").
    /// Prefer EventHandler&lt;TaskEventArgs&gt; over empty EventArgs when payload matters.
    /// </summary>
    public class TaskEventArgs : EventArgs
    {
        public required string Title { get; init; }
    }

    /// <summary>
    /// Publisher: owns the events and is the only type allowed to raise them.
    ///
    /// event EventHandler&lt;T&gt;? → outsiders may only += / -= subscribe.
    /// They cannot assign null or Invoke from outside (unlike a public delegate field).
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

        protected virtual void OnTaskCreated(TaskEventArgs e) => TaskCreated?.Invoke(this, e);

        protected virtual void OnTaskCompleted(TaskEventArgs e) => TaskCompleted?.Invoke(this, e);
    }
}

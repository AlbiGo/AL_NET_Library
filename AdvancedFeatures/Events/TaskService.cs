namespace AdvancedFeatures.Events
{
    /// <summary>
    /// Publisher: raises TaskCreated / TaskCompleted.
    /// Events are multicast delegates with restricted access — only this type can raise them.
    /// </summary>
    public class TaskService
    {
        public event EventHandler? TaskCreated;
        public event EventHandler? TaskCompleted;

        public void PrepareTask(TaskItem work)
        {
            Console.WriteLine($"Preparing '{work.Title}'...");
            OnTaskCreated();
        }

        public void CompleteTask(TaskItem work)
        {
            Console.WriteLine($"Completed '{work.Title}'.");
            OnTaskCompleted();
        }

        protected virtual void OnTaskCreated() => TaskCreated?.Invoke(this, EventArgs.Empty);

        protected virtual void OnTaskCompleted() => TaskCompleted?.Invoke(this, EventArgs.Empty);
    }
}

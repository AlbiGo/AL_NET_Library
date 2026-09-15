namespace Threads.Threads
{
    /// <summary>
    /// Shared-state demo — Prefer synchronize; Avoid bare increments from many threads.
    /// <para>
    /// Kitchen shows overlapping <b>independent</b> work. This shows what goes wrong when
    /// threads share mutable state: <c>count++</c> is not atomic (read/modify/write), so
    /// increments disappear. Prefer <c>lock</c> (or <see cref="Interlocked"/>) around the update.
    /// </para>
    /// <para>
    /// <b>What <c>lock</c> is:</b> a mutual-exclusion gate. Only one thread at a time may enter
    /// the block that locks on the same object (<c>gate</c>). Others wait until the first thread
    /// leaves the block (releases the lock). That turns a racy <c>count++</c> into a safe critical section.
    /// Under the hood, <c>lock (gate) { … }</c> is <c>Monitor.Enter</c> / <c>try/finally Monitor.Exit</c>.
    /// </para>
    /// </summary>
    public static class SharedCounterDemo
    {
        private const int Threads = 8;
        private const int IncrementsPerThread = 50_000;
        private const int Expected = Threads * IncrementsPerThread;

        /// <summary>
        /// Avoid: unsynchronized <c>count++</c> from many threads — lost updates (total &lt; expected).
        /// </summary>
        public static void RunUnsafe()
        {
            var count = 0;
            var workers = new Thread[Threads];

            for (var i = 0; i < Threads; i++)
            {
                workers[i] = new Thread(() =>
                {
                    for (var n = 0; n < IncrementsPerThread; n++)
                        count++; // not thread-safe: two threads can read the same value
                });
                workers[i].Start();
            }

            foreach (var worker in workers)
                worker.Join();

            Console.WriteLine($"Avoid (no lock):  count={count:N0}  expected={Expected:N0}  lost={Expected - count:N0}");
        }

        /// <summary>
        /// Prefer: protect the shared update with <c>lock</c> so only one thread mutates at a time.
        /// <para>
        /// <c>gate</c> is a dedicated lock object (do not lock on <c>this</c> or a public type —
        /// other code could lock the same instance and deadlock). Keep the locked region small:
        /// only the shared mutation, not slow I/O.
        /// </para>
        /// </summary>
        public static void RunWithLock()
        {
            var count = 0;
            // Private object used only as the lock identity — not the counter itself.
            var gate = new object();
            var workers = new Thread[Threads];

            for (var i = 0; i < Threads; i++)
            {
                workers[i] = new Thread(() =>
                {
                    for (var n = 0; n < IncrementsPerThread; n++)
                    {
                        // Enter: wait until no other thread holds gate, then take it.
                        // Exit: release when leaving the block (even if an exception is thrown).
                        lock (gate)
                            count++;
                    }
                });
                workers[i].Start();
            }

            foreach (var worker in workers)
                worker.Join();

            Console.WriteLine($"Prefer (lock):    count={count:N0}  expected={Expected:N0}  ok={count == Expected}");
        }

        /// <summary>
        /// Prefer (alternative): <see cref="Interlocked.Increment"/> for a single integer —
        /// CPU-level atomic update, no explicit lock object. Use <c>lock</c> when the critical
        /// section is bigger than one field (several fields that must stay consistent together).
        /// </summary>
        public static void RunWithInterlocked()
        {
            var count = 0;
            var workers = new Thread[Threads];

            for (var i = 0; i < Threads; i++)
            {
                workers[i] = new Thread(() =>
                {
                    for (var n = 0; n < IncrementsPerThread; n++)
                        Interlocked.Increment(ref count);
                });
                workers[i].Start();
            }

            foreach (var worker in workers)
                worker.Join();

            Console.WriteLine($"Prefer (Interlocked): count={count:N0}  expected={Expected:N0}  ok={count == Expected}");
        }
    }
}

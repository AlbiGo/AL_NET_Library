namespace Threads.Threads
{
    /// <summary>
    /// Baseline kitchen — every step runs one after another on the calling thread.
    /// <para>
    /// <b>Why a kitchen?</b> Cooking has steps everyone understands: some can happen at the same
    /// time (boil water while chopping), and some cannot (pasta waits for boiling water).
    /// That maps cleanly to concurrency Prefer/Avoid without sockets, databases, or fake “work.”
    /// Total time ≈ sum of delays — the timing yardstick for <see cref="KitchenThread"/> /
    /// <see cref="KitchenAsync"/>.
    /// </para>
    /// </summary>
    public class Kitchen
    {
        // Keep short for demos; increase to make timing differences obvious.
        protected static readonly TimeSpan StepDelay = TimeSpan.FromMilliseconds(300);

        public virtual void MakePasta()
        {
            BoilWater();
            PrepareIngredients();
            MakeSauce();
            PourPastaIntoWater();
            PourSauce();
            PrepareTable();
            FillPlates();
        }

        protected virtual void BoilWater()
        {
            Console.WriteLine(nameof(BoilWater));
            Thread.Sleep(StepDelay); // Blocks the thread (contrast with Task.Delay in KitchenAsync).
        }

        protected virtual void PrepareIngredients()
        {
            Console.WriteLine(nameof(PrepareIngredients));
            Thread.Sleep(StepDelay);
        }

        protected virtual void MakeSauce()
        {
            Console.WriteLine(nameof(MakeSauce));
            Thread.Sleep(StepDelay);
        }

        protected virtual void PourPastaIntoWater()
        {
            Console.WriteLine(nameof(PourPastaIntoWater));
            Thread.Sleep(StepDelay);
        }

        protected virtual void PrepareTable()
        {
            Console.WriteLine(nameof(PrepareTable));
            Thread.Sleep(StepDelay);
        }

        protected virtual void PourSauce()
        {
            Console.WriteLine(nameof(PourSauce));
            Thread.Sleep(StepDelay);
        }

        protected virtual void FillPlates()
        {
            Console.WriteLine(nameof(FillPlates));
            Thread.Sleep(StepDelay);
        }
    }

    /// <summary>
    /// Threaded kitchen — overlap independent work; <c>Join</c> enforces real dependencies.
    /// <para>
    /// Boil water / chop / set table can run together; pasta waits for water; sauce pour waits for sauce.
    /// That dependency story is why this example exists — not “use more threads always.”
    /// </para>
    /// </summary>
    public class KitchenThread : Kitchen
    {
        public override void MakePasta()
        {
            var boilWater = new Thread(BoilWater);
            var prepareIngredients = new Thread(PrepareIngredients);
            var prepareTable = new Thread(PrepareTable);
            var makeSauce = new Thread(MakeSauce);

            // These three do not depend on each other — start together.
            Console.WriteLine("Parallel: boil water, prepare ingredients, prepare table");
            boilWater.Start();
            prepareIngredients.Start();
            prepareTable.Start();

            // Sauce needs ingredients first.
            prepareIngredients.Join();
            makeSauce.Start();

            // Pasta needs boiled water first.
            boilWater.Join();
            Console.WriteLine("Water ready — pouring pasta");
            var pourPasta = new Thread(PourPastaIntoWater);
            pourPasta.Start();
            pourPasta.Join();

            makeSauce.Join();
            prepareTable.Join();

            Console.WriteLine("Sauce ready — pouring sauce");
            var pourSauce = new Thread(PourSauce);
            pourSauce.Start();
            pourSauce.Join();

            var fillPlates = new Thread(FillPlates);
            fillPlates.Start();
            fillPlates.Join();
        }
    }

    /// <summary>
    /// Async kitchen — same dependency graph as <see cref="KitchenThread"/>, with <c>async</c>/<c>await</c>.
    /// <para>
    /// <c>Task.Delay</c> is a true async wait (does not block a thread like <c>Thread.Sleep</c>).
    /// Starting tasks without awaiting yet lets independent work overlap. Methods are named <c>*Async</c>.
    /// </para>
    /// </summary>
    public class KitchenAsync
    {
        private static readonly TimeSpan StepDelay = TimeSpan.FromMilliseconds(300);

        public async Task MakePastaAsync()
        {
            Console.WriteLine("Parallel: boil water, prepare ingredients, prepare table");
            // Start three operations; they run concurrently until we await them.
            var boilWater = BoilWaterAsync();
            var prepareIngredients = PrepareIngredientsAsync();
            var prepareTable = PrepareTableAsync();

            await prepareIngredients;
            var makeSauce = MakeSauceAsync();

            await boilWater;
            Console.WriteLine("Water ready — pouring pasta");
            await PourPastaIntoWaterAsync();

            await makeSauce;
            await prepareTable;

            Console.WriteLine("Sauce ready — pouring sauce");
            await PourSauceAsync();
            await FillPlatesAsync();
        }

        private static async Task BoilWaterAsync()
        {
            Console.WriteLine(nameof(BoilWaterAsync));
            await Task.Delay(StepDelay);
        }

        private static async Task PrepareIngredientsAsync()
        {
            Console.WriteLine(nameof(PrepareIngredientsAsync));
            await Task.Delay(StepDelay);
        }

        private static async Task MakeSauceAsync()
        {
            Console.WriteLine(nameof(MakeSauceAsync));
            await Task.Delay(StepDelay);
        }

        private static async Task PourPastaIntoWaterAsync()
        {
            Console.WriteLine(nameof(PourPastaIntoWaterAsync));
            await Task.Delay(StepDelay);
        }

        private static async Task PrepareTableAsync()
        {
            Console.WriteLine(nameof(PrepareTableAsync));
            await Task.Delay(StepDelay);
        }

        private static async Task PourSauceAsync()
        {
            Console.WriteLine(nameof(PourSauceAsync));
            await Task.Delay(StepDelay);
        }

        private static async Task FillPlatesAsync()
        {
            Console.WriteLine(nameof(FillPlatesAsync));
            await Task.Delay(StepDelay);
        }
    }
}

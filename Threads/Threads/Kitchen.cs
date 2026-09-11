namespace Threads.Threads
{
    /// <summary>
    /// Sequential cooking — each step blocks the caller.
    /// </summary>
    public class Kitchen
    {
        // Short delays so demos finish quickly; raise for clearer timing experiments.
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
            Thread.Sleep(StepDelay);
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
    /// Overlaps independent steps with threads; joins enforce real dependencies
    /// (e.g. pasta goes in only after water has boiled).
    /// </summary>
    public class KitchenThread : Kitchen
    {
        public override void MakePasta()
        {
            var boilWater = new Thread(BoilWater);
            var prepareIngredients = new Thread(PrepareIngredients);
            var prepareTable = new Thread(PrepareTable);
            var makeSauce = new Thread(MakeSauce);

            Console.WriteLine("Parallel: boil water, prepare ingredients, prepare table");
            boilWater.Start();
            prepareIngredients.Start();
            prepareTable.Start();

            prepareIngredients.Join();
            makeSauce.Start();

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
    /// Same dependency graph as <see cref="KitchenThread"/> using async/await and Task.WhenAll.
    /// </summary>
    public class KitchenAsync
    {
        private static readonly TimeSpan StepDelay = TimeSpan.FromMilliseconds(300);

        public async Task MakePastaAsync()
        {
            Console.WriteLine("Parallel: boil water, prepare ingredients, prepare table");
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

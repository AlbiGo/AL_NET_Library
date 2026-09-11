using System.Diagnostics;
using Threads.Threads;

Console.WriteLine("=== Sequential Kitchen ===");
var sequential = new Kitchen();
var sw1 = Stopwatch.StartNew();
sequential.MakePasta();
sw1.Stop();
Console.WriteLine($"Elapsed: {sw1.ElapsedMilliseconds} ms");
Console.WriteLine();

Console.WriteLine("=== Threaded Kitchen ===");
var threaded = new KitchenThread();
var sw2 = Stopwatch.StartNew();
threaded.MakePasta();
sw2.Stop();
Console.WriteLine($"Elapsed: {sw2.ElapsedMilliseconds} ms");
Console.WriteLine();

Console.WriteLine("=== Async Kitchen ===");
var asyncKitchen = new KitchenAsync();
var sw3 = Stopwatch.StartNew();
await asyncKitchen.MakePastaAsync();
sw3.Stop();
Console.WriteLine($"Elapsed: {sw3.ElapsedMilliseconds} ms");

// Catch only what you handle. Prefer throw; over throw ex; to keep the stack trace.

Console.WriteLine("=== Exceptions ===");

for (int i = 0; i < 10; i++)
{
    try
    {
        ProcessIndex(i);
    }
    catch (InvalidOperationException ex)
    {
        // Handled: log and continue the loop.
        Console.WriteLine($"Handled at index {i}: {ex.Message}");
    }
}

Console.WriteLine();
Console.WriteLine("=== Rethrow correctly ===");
try
{
    RethrowDemo();
}
catch (Exception ex)
{
    Console.WriteLine($"Caught after rethrow: {ex.Message}");
    Console.WriteLine($"Stack still points at ThrowInner: {ex.StackTrace?.Contains(nameof(ThrowInner)) == true}");
}

static void ProcessIndex(int index)
{
    if (index == 6)
    {
        throw new InvalidOperationException("Simulated failure at index 6");
    }

    Console.WriteLine($"Multiply {index * index}");
}

static void RethrowDemo()
{
    try
    {
        ThrowInner();
    }
    catch (Exception)
    {
        // Preserves the original stack trace. Do not use: throw ex;
        throw;
    }
}

static void ThrowInner()
{
    throw new InvalidOperationException("Inner failure");
}

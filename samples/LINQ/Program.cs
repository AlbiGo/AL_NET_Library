using LINQ.Lamda;

// Prefer IQueryable filters, 1-based pagination, and joins that project both sides.

Console.WriteLine("=== LINQ (in-memory illustration of the same rules) ===");

var entities = new[]
{
    new SampleEntity { Id = 1, Name = "Alpha", RelatedName = "R1" },
    new SampleEntity { Id = 2, Name = "Beta", RelatedName = "R2" },
    new SampleEntity { Id = 3, Name = "Alpine", RelatedName = "R3" },
    new SampleEntity { Id = 4, Name = "Gamma", RelatedName = "R4" }
}.AsQueryable();

var filter = new EntityFilter { Name = "Al", Page = 1, Size = 10 };

var page = entities
    .Where(e => string.IsNullOrWhiteSpace(filter.Name) || e.Name.Contains(filter.Name))
    .Skip((filter.Page - 1) * filter.Size)
    .Take(filter.Size);

Console.WriteLine("Filtered page:");
foreach (var item in page)
{
    Console.WriteLine($"  {item.Id} | {item.Name} -> {item.RelatedName}");
}

Console.WriteLine();
Console.WriteLine("EF-backed helpers live in Lamda/LamdaMethods.cs (DatabaseContext).");

file class SampleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RelatedName { get; set; } = string.Empty;
}

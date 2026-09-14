using DataManagement.DbContext;
using DataManagement.Entities;
using DataManagement.Queries;
using DataManagement.Repositories.Implementations;
using DataManagement.Repositories.Interfaces;

Console.WriteLine("=== Data management: soft-delete related ===");

IBaseRepository<Entity1> entities = new BaseRepository<Entity1>();
IBaseRepository<Entity2> related = new BaseRepository<Entity2>();
IBaseRepository<Entity3> children3 = new BaseRepository<Entity3>();
IBaseRepository<Entity4> children4 = new BaseRepository<Entity4>();

await related.Add(new Entity2 { Id = 6, Created = DateTime.UtcNow, Name = "Related", Desc = "Test" });
await entities.Add(new Entity1 { Id = 1, Created = DateTime.UtcNow, Name = "Root", Entity2ID = 6 });
await children3.Add(new Entity3 { Id = 1, Name = "C3-1", Entity1ID = 1 });
await children3.Add(new Entity3 { Id = 2, Name = "C3-2", Entity1ID = 1 });
await children4.Add(new Entity4 { Id = 1, Name = "C4-1", Entity1ID = 1 });
await children4.Add(new Entity4 { Id = 2, Name = "C4-2", Entity1ID = 1 });

Console.WriteLine("Before soft deletion:");
PrintDeletedFlags();

var itemToRemove = entities.CustomQueryNT().First(p => p.Id == 1);
await entities.SoftRemoveRelated(itemToRemove, new[] { "Entity2", "Entity3s", "Entity4s" });

Console.WriteLine();
Console.WriteLine("After soft deletion:");
PrintDeletedFlags();

Console.WriteLine();
Console.WriteLine("=== QueryBuilder (SQL + parameters, no string replace) ===");
var built = QueryBuilder.BuildQuery(
    "Entity1_query",
    new List<Param>
    {
        new() { Name = "@nameParam", ParamType = ParamType.String, Value = "%Test%" }
    });
Console.WriteLine(built.Sql.Trim());
foreach (var pair in built.Parameters)
{
    Console.WriteLine($"  {pair.Key} = {pair.Value}");
}

static void PrintDeletedFlags()
{
    var e1 = new BaseRepository<Entity1>().CustomQueryNT().FirstOrDefault();
    var e2 = new BaseRepository<Entity2>().CustomQueryNT().FirstOrDefault();
    var e3 = new BaseRepository<Entity3>().CustomQueryNT().ToList();
    var e4 = new BaseRepository<Entity4>().CustomQueryNT().ToList();

    Console.WriteLine($"Entity1 Deleted: {e1?.Deleted}");
    Console.WriteLine($"Entity2 Deleted: {e2?.Deleted}");
    e3.ForEach(p => Console.WriteLine($"Entity3 {p.Id} Deleted: {p.Deleted}"));
    e4.ForEach(p => Console.WriteLine($"Entity4 {p.Id} Deleted: {p.Deleted}"));
}

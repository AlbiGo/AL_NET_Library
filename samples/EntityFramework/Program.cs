using AuditEntry;
using EntityFramework.Detach.Repository;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== EF detach ===");
// Detached entities are ignored by SaveChanges — local edits will not persist.

await using var context = new AuditDbContext("detach-demo");
var repo = new BaseRepository<User>(context);

await repo.Add(new User { Email = "one@example.com", Name = "One" });
await repo.Add(new User { Email = "two@example.com", Name = "Two" });

Console.WriteLine("Before detach/edit:");
foreach (var u in await repo.CustomQuery().ToListAsync())
{
    Console.WriteLine($"  {u.Id} | {u.Email}");
}

var user = await repo.CustomQuery().FirstAsync(p => p.Email == "one@example.com");
user.Email = "changed@example.com";
repo.Detach(user);
await repo.SaveChanges();

Console.WriteLine();
Console.WriteLine("After edit + detach + SaveChanges (change should NOT persist):");
foreach (var u in await repo.CustomQuery().AsNoTracking().ToListAsync())
{
    Console.WriteLine($"  {u.Id} | {u.Email}");
}

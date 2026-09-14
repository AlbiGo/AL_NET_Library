using AuditEntry;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== Audit entry ===");
// Saving users records property-level old/new values into AuditEntry / AuditEntryProperty.

await using var dbContext = new AuditDbContext("audit-demo");

dbContext.Users.AddRange(
    new User { Email = "one@example.com", Name = "One" },
    new User { Email = "two@example.com", Name = "Two" });
await dbContext.SaveChangesAsync();

var user = await dbContext.Users.FirstAsync();
user.Email = "updated@example.com";
await dbContext.SaveChangesAsync();

Console.WriteLine("Users:");
foreach (var u in dbContext.Users)
{
    Console.WriteLine($"  {u.Id} | {u.Email}");
}

Console.WriteLine();
Console.WriteLine("Audit properties:");
var auditRows = await dbContext.AuditEntryProperties
    .Include(p => p.AuditEntry)
    .ToListAsync();

foreach (var row in auditRows)
{
    Console.WriteLine(
        $"{row.AuditEntry.EntityName} | {row.PropertyName} | {row.PropertyOldValue} -> {row.PropertyNewValue} | {row.Modified}");
}

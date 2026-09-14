using System.ComponentModel.DataAnnotations.Schema;

namespace AuditEntry
{
    public class AuditEntryProperty
    {
        public int Id { get; set; }

        [ForeignKey(nameof(AuditEntry))]
        public int? AuditEntryID { get; set; }

        public AuditEntry? AuditEntry { get; set; }
        public string? PropertyName { get; set; }
        public string? PropertyOldValue { get; set; }
        public string? PropertyNewValue { get; set; }
        public string? Modified { get; set; }
    }
}

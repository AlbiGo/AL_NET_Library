namespace AuditEntry
{
    public class AuditEntry
    {
        public int ID { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public int EntityID { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; } = string.Empty;
        public int UserId { get; set; }
        public List<AuditEntryProperty> AuditEntryProperties { get; set; } = new();
    }
}

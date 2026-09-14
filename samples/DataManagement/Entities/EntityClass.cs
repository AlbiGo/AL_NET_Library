using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManagement.Entities
{
    public class Entity1 : BaseEntity
    {
        [ForeignKey("Entity2")]
        public int Entity2ID { get; set; }

        public Entity2 Entity2 { get; set; }

        public List<Entity3> Entity3s { get; set; }

        public List<Entity4> Entity4s { get; set; }
    }

    public class Entity2 : BaseEntity
    {
        public string Desc { get; set; }

        public Entity5 Entity5 { get; set; }
    }

    public class Entity3 : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string? Desc { get; set; }

        [ForeignKey("Entity1")]
        public int Entity1ID { get; set; }

        public Entity1 Entity1 { get; set; }
    }

    public class Entity4 : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string? Desc { get; set; }

        [ForeignKey("Entity1")]
        public int Entity1ID { get; set; }

        public Entity1 Entity1 { get; set; }
    }

    public class Entity5 : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string? Desc { get; set; }

        [ForeignKey("Entity2")]
        public int Entity1ID { get; set; }

        public Entity2 Entity2 { get; set; }
    }

    public class Log : BaseEntity
    {
        public string LogMessage { get; set; }
        public LogType Type { get; set; }
    }

    public enum LogType
    {
        Info = 1,
        Warning = 2,
        Error = 3
    }
}
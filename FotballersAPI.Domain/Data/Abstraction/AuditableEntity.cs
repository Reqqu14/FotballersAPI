namespace FotballersAPI.Domain.Data.Abstraction
{
    public abstract class AuditableEntity
    {
        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset ModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}

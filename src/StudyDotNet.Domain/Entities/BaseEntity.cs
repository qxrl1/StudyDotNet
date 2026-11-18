namespace StudyDotNet.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id              { get; set; }
    public DateTime CreatedAt   { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
}

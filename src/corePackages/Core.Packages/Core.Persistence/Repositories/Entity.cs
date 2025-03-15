namespace Core.Persistence.Repositories;

public class Entity<TId> : IEntityTimestamps
{
    public Entity()
    {
        Id = default; // Hiç bir değer atanmaz ise o id değerinin default hali atanacak
    }

    public Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
namespace KebabFury.Innopolice.WebApi.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Guid id, Type entityType): base($"Entity {entityType} with Id: {id} is not found")
    {
        
    }
}
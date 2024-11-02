namespace KebabFury.Innopolice.WebApi.Application.Exceptions;

public class ProviderNotFoundException : Exception
{
    public ProviderNotFoundException(string name): base($"Provider by name {name} is not found!")
    {
        
    }
}
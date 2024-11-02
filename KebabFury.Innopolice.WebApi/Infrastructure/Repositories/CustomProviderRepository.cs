using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Context;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Repositories;

public class CustomProviderRepository : BaseRepository<CustomProvider>
{
    public CustomProviderRepository(DataContext context) : base(context, "providers")
    {
    }
}
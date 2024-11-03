using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Domain.Models;
using KebabFury.Innopolice.WebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface ICustomProviderService : IBaseService<CustomProvider>
{
    Task<IList<CustomProvider>> ListByUserId(Guid userId);
    Task CreateCustomProviderAsync(CreateCustomProviderRequest request);
    Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations();
    Task DeepUpdateAsync(Guid id, CreateCustomProviderRequest request);
}

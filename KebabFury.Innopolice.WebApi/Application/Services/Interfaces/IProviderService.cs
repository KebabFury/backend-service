using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using KebabFury.Innopolice.WebApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IProviderService : IBaseService<CustomProvider>
{
    Task<AuthorizeResultDto> Authorize(string providerName);
    Task<string> CallbackAsync(string providerName, string? code = null, string? state = null);
    Task CreateCustomAsync(CreateCustomProviderRequest request);
    Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations();
    Task<JsonResult> SaveAuthorizationDataAndReturnResponse(string authorizationData, string systemName);
}

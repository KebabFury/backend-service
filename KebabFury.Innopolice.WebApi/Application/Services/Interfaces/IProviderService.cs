using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface IProviderService
{
    Task<JsonResult> SaveAuthorizationDataAndReturnResponse(string authorizationData, string systemName);
    Task CreateCustomAsync(CreateCustomProviderRequest request);
    Task<IList<CustomProviderDocumentationDto>> ListAllDocumentations();
}

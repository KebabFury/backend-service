using KebabFury.Innopolice.WebApi.Application.Dto.Provider;
using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services.Interfaces;

public interface ICustomProviderAuthorizationService
{
    Task<AuthorizeResultDto> Authorize(string providerName);
    Task<string> CallbackAsync(string providerName, string? code = null);
    Task<JsonResult> SaveAuthorizationDataAndReturnResponse(string authorizationData, string systemName);
}
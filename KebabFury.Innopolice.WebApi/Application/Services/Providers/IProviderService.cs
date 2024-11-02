using Microsoft.AspNetCore.Mvc;

namespace KebabFury.Innopolice.WebApi.Application.Services.Providers;

public interface IProviderService
{
    Task<JsonResult> SaveAuthorizationDataAndReturnResponse(string authorizationData, string systemName);
}

namespace KebabFury.Innopolice.TodoIst.Services;

public interface ITodoIstAuthorizationService
{
    string Authorize();
    Task<string> CallbackAsync(string code = null, string state = null, string error = null);
}
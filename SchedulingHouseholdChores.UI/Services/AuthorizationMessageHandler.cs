namespace SchedulingHouseholdChores.UI.Services;

/// <summary>
/// Este handler será usado quando precisarmos adicionar Authorization automaticamente a requisições.
/// Por enquanto, deixamos desabilitado para evitar dependências cíclicas.
/// </summary>
public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly AuthClientService _authService;

    public AuthorizationMessageHandler(AuthClientService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _authService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

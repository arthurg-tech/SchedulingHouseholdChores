using System.Net.Http.Headers;

namespace SchedulingHouseholdChores.UI.Services;

/// <summary>
/// Handler customizado que automaticamente adiciona o token JWT em todas as requisições.
/// Usa IServiceProvider para access lazy ao AuthClientService, evitando circular dependency.
/// </summary>
public class AuthHttpMessageHandler : HttpClientHandler
{
    private readonly IServiceProvider _serviceProvider;

    public AuthHttpMessageHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Acessa o serviço de forma lazy para evitar circular dependency
        var authService = _serviceProvider.GetService<AuthClientService>();
        if (authService != null)
        {
            var token = await authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

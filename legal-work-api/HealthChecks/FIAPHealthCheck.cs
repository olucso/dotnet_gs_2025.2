using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace legal_work_api.HealthChecks
{
    public class FIAPHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = "https://www.fiap.com.br";

                using HttpClient client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true });

                using var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                    return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, "Sistema funcionando."));
                else
                    return Task.FromResult(new HealthCheckResult(HealthStatus.Degraded, "O sistema não está funcionando."));
            }
            catch (Exception)
            {
                return Task.FromResult(new HealthCheckResult(HealthStatus.Unhealthy, "Sistema fora do ar."));
            }
        }
    }
}

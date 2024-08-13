using NBomber.CSharp;

class Program
{
    static void Main(string[] args)
    {
        var httpClient = new HttpClient();

        var scenario = Scenario.Create("consolidado_diario_500_requests_por_segundo_scenario", async context =>
        {
            var response = await httpClient.GetAsync("http://127.0.0.1:6001/api/Lancamento/consolidado-diario?dataLancamento=2024-08-12");

            return response.IsSuccessStatusCode
                ? Response.Ok()
                : Response.Fail();
        })
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 500, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(60))
            );


        var stats = NBomberRunner
            .RegisterScenarios(scenario)
            .Run();

        var totalRequests = stats.ScenarioStats[0].AllRequestCount;
        var failedRequests = stats.ScenarioStats[0].AllFailCount;
        var failPercentage = (failedRequests / (double)totalRequests) * 100;

        if (failPercentage > 5)
        {
            Console.WriteLine($"Teste falhou: {failPercentage}% das requisições falharam.");
        }
        else
        {
            Console.WriteLine($"Teste bem-sucedido: {failPercentage}% das requisições falharam.");
        }
    }
}
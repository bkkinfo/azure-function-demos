using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParallelDurableFunction
{
    public static class StarterFunction1
    {
        [FunctionName("StarterFunction1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]
            HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            TraceWriter log)
        {
            log.Info("About to start orchestration");

            var orchestrationId = await starter.StartNewAsync("DurableFunction1", log);
            return starter.CreateCheckStatusResponse(req, orchestrationId);
        }
    }
}

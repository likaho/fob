using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Threading.Tasks;

namespace ProsperChainFunctions
{
    //http://localhost:7071/api/EthereumRetreive?Id=2&submissionDate=200315
    public static class EthereumRetreive
    {
        [FunctionName("EthereumRetreive")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string retreiveFunctionReturn = string.Empty;

            try
            {
                var contractHandler = EthereumHelper.GetContractHandler();
                var retreiveFunction = new RetreiveFunction();
                retreiveFunction.Id = Convert.ToUInt32(req.Query["id"]);
                retreiveFunction.SubmissionDate = Convert.ToUInt32(req.Query["submissionDate"]);
                retreiveFunctionReturn = await contractHandler.QueryAsync<RetreiveFunction, string>(retreiveFunction);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                log.LogInformation(ex.StackTrace);
                retreiveFunctionReturn = ex.StackTrace;
            }

            return new OkObjectResult(retreiveFunctionReturn);
        }
    }

    public partial class RetreiveFunction : RetreiveFunctionBase { }

    [Function("retreive", "string")]
    public class RetreiveFunctionBase : FunctionMessage
    {
        [Parameter("uint32", "id", 1)]
        public virtual uint Id { get; set; }
        [Parameter("uint32", "submissionDate", 2)]
        public virtual uint SubmissionDate { get; set; }
    }

}

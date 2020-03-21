using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace ProsperChainFunctions
{
    public static class EthereumCount
    {
        [FunctionName("EthereumCount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            BigInteger countFunctionReturn;

            try
            {
                var contractHandler = EthereumHelper.GetContractHandler();
                countFunctionReturn = await contractHandler.QueryAsync<GetCountFunction, BigInteger>();
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                log.LogInformation(ex.StackTrace);
            }

            return new OkObjectResult(countFunctionReturn);
        }
    }

    public partial class GetCountFunction : GetCountFunctionBase { }

    [Function("getCount", "uint256")]
    public class GetCountFunctionBase : FunctionMessage
    {

    }

}

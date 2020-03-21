using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProsperChainFunctions
{
    public static class EthereumStore
    {
        [FunctionName("EthereumStore")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string transactionId = string.Empty;
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");
                var contractHandler = EthereumHelper.GetContractHandler();

                var content = await new StreamReader(req.Body).ReadToEndAsync();
                var storeFunction = JsonConvert.DeserializeObject<StoreFunction>(content);

                var storeFunctionTxnReceipt = await contractHandler.SendRequestAndWaitForReceiptAsync(storeFunction);
                transactionId =  storeFunctionTxnReceipt.TransactionIndex.ToString();

            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                log.LogInformation(ex.StackTrace);
                transactionId = ex.StackTrace;
            }

            return new OkObjectResult(transactionId);
        }
    }

    public partial class StoreFunction : StoreFunctionBase { }

    [Function("store")]
    public class StoreFunctionBase : FunctionMessage
    {
        [Parameter("uint32", "id", 1)]
        public virtual uint Id { get; set; }
        [Parameter("uint32", "submissionDate", 2)]
        public virtual uint SubmissionDate { get; set; }
        [Parameter("string", "hash", 3)]
        public virtual string Hash { get; set; }
    }

}

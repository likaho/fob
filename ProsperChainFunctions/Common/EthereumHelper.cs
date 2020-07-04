using Nethereum.Contracts.ContractHandlers;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProsperChainFunctions
{
    public class EthereumHelper
    {
        private const string url = "https://rinkeby.infura.io/v3/723821101034................";
        private const string privateKey = "A482B8A77F913DB6D6C3D21375CC886..................";
        private const string contractAddress = "0xa9066f7C218251a16e28781.......................";

        public static ContractHandler GetContractHandler()
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var web3 = new Web3(account, url);
            return web3.Eth.GetContractHandler(contractAddress);
        }
    }
}

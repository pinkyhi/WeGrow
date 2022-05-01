using Newtonsoft.Json;
using WeGrow.Core.Resources;
using WeGrow.LiqPay.Interfaces;
using WeGrow.LiqPay.Models;
using WeGrow.LiqPay.Resolvers;

namespace WeGrow.LiqPay.Services
{
    public class LiqPayService : ILiqPayService
    {
        private readonly LiqPayOptions liqPayOptions;

        public LiqPayService(IConfiguration configuration)
        {
            this.liqPayOptions = new LiqPayOptions();
            configuration.GetSection(nameof(LiqPayOptions)).Bind(this.liqPayOptions);
        }

        public LiqPayAnswerModel AnswerModelFromData(string data)
        {
            string decodedData = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(data));
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            settings.NullValueHandling = NullValueHandling.Ignore;
            LiqPayAnswerModel model = JsonConvert.DeserializeObject<LiqPayAnswerModel>(decodedData, settings);
            return model;
        }

        public bool CheckDataBySignature(string data, string signature, string private_key = null)
        {
            string serverSignature = this.CreateSignature(data, private_key ?? liqPayOptions.PrivateKey);
            return serverSignature.Equals(signature);
        }

        public (string data, string signature) EncryptLiqPay(LiqPayCheckoutModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Public_Key))
            {
                model.Public_Key = liqPayOptions.PublicKey;
            }
            if (string.IsNullOrWhiteSpace(model.Private_Key))
            {
                model.Private_Key = liqPayOptions.PrivateKey;
            }
            if (!string.IsNullOrEmpty(liqPayOptions.ServerUrl))
            {
                model.Server_Url = liqPayOptions.ServerUrl + ApiRoutes.LiqPayNotification;
            }
            string data = this.CreateData(model);
            string signature = this.CreateSignature(data, model.Private_Key);
            return (data, signature);

        }

        private string CreateData(LiqPayCheckoutModel model)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowercaseContractResolver();
            settings.NullValueHandling = NullValueHandling.Ignore;
            string jsonString = JsonConvert.SerializeObject(model, Formatting.None, settings);
            string data = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonString));
            return data;
        }

        private string CreateSignature(string data, string private_key)
        {
            string signString = private_key + data + private_key;
            byte[] signStringBytes = System.Text.Encoding.UTF8.GetBytes(signString);
            using (var sha = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha.ComputeHash(signStringBytes);
                string signature = Convert.ToBase64String(hash);
                return signature;
            }
        }
    }
}

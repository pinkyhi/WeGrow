using WeGrow.LiqPay.Models;

namespace WeGrow.LiqPay.Interfaces
{
    public interface ILiqPayService
    {
        public (string data, string signature) EncryptLiqPay(LiqPayCheckoutModel model);

        public bool CheckDataBySignature(string data, string signature, string private_key = null);

        public LiqPayAnswerModel AnswerModelFromData(string data);
    }
}

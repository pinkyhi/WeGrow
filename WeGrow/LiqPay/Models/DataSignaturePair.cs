using System.ComponentModel.DataAnnotations;

namespace WeGrow.LiqPay.Models
{
    public class DataSignaturePair
    {
        public DataSignaturePair()
        {}
        public DataSignaturePair(string data, string signature)
        {
            Data = data;
            Signature = signature;
        }
        [Required]
        public string Data { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}

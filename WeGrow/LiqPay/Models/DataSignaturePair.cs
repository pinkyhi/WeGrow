using System.ComponentModel.DataAnnotations;

namespace WeGrow.LiqPay.Models
{
    public class DataSignaturePair
    {
        [Required]
        public string Data { get; set; }

        [Required]
        public string Signature { get; set; }
    }
}

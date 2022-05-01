namespace WeGrow.LiqPay.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LiqPayCheckoutModel
    {
        // Required
        public int Version { get; set; } = 3;

        public string Public_Key { get; set; }

        public string Private_Key { get; set; }

        public string Action { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public string Order_Id { get; set; }

        // Non-required
        public string Expired_Date { get; set; }

        public string Language { get; set; }

        public string PayTypes { get; set; }

        public string Result_Url { get; set; }

        public string Server_Url { get; set; }

        public string VerifyCode { get; set; }

        // Additional
        public string Info { get; set; }

        [StringLength(25)]
        public string Product_Category { get; set; }

        [StringLength(500)]
        public string Product_Description { get; set; }

        [StringLength(100)]
        public string Product_Name { get; set; }

        [StringLength(2000)]
        public string Product_Url { get; set; }

        /// <summary>
        /// Test mode: 1 - yes, 0 - no. (in test mode money isn't withdrawn)
        /// </summary>
        public int Sandbox { get; set; }
    }
}

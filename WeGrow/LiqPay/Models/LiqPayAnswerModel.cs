namespace WeGrow.LiqPay.Models
{
    public class LiqPayAnswerModel
    {
        public int? Acq_Id { get; set; }

        public string Action { get; set; }

        public decimal? Agent_Commission { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Amount_Bonus { get; set; }

        public decimal? Amount_Credit { get; set; }

        public decimal? Amount_Debit { get; set; }

        public string Authcode_Credit { get; set; }

        public string Authcode_Debit { get; set; }

        public string Card_Token { get; set; }

        public decimal? Commission_Credit { get; set; }

        public decimal? Commision_Debit { get; set; }

        public string Compltion_Date { get; set; }

        public string Create_Date { get; set; }

        public string Currency { get; set; }

        public string Currency_Credit { get; set; }

        public string Currency_Debit { get; set; }

        public string Customer { get; set; }

        public string Description { get; set; }

        public string End_Date { get; set; }

        public string Err_Code { get; set; }

        public string Err_Description { get; set; }

        public string Info { get; set; }

        public string Ip { get; set; }

        public bool Is_3ds { get; set; }

        public string Liqpay_Order_Id { get; set; }

        public int? Mpi_Eci { get; set; }

        public string Order_Id { get; set; }

        public int? Payment_Id { get; set; }

        public string PayType { get; set; }

        public string Public_Key { get; set; }

        public decimal? Receiver_Commission { get; set; }

        public string Redirect_To { get; set; }

        public string Refund_Date_Last { get; set; }

        public string Rrn_Credit { get; set; }

        public string Rrn_Debit { get; set; }

        public decimal? Sender_Bonus { get; set; }

        public string Sender_Card_Bank { get; set; }

        public string Sender_Card_Country { get; set; }

        public string Sender_Card_Mask2 { get; set; }

        public string Sender_Card_Type { get; set; }

        public decimal? Sender_Commision { get; set; }

        public string Sender_First_Nam { get; set; }

        public string Sender_Last_Name { get; set; }

        public string Sender_Phone { get; set; }

        public string Status { get; set; }

        public string Token { get; set; }

        public string Type { get; set; }

        public int? Version { get; set; }

        public string Err_Erc { get; set; }

        public string Product_Category { get; set; }

        public string Product_Description { get; set; }

        public string Product_Name { get; set; }

        public string Product_Url { get; set; }

        public decimal? Refund_Amount { get; set; }

        public string VerifyCode { get; set; }
    }
}

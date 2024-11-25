namespace WebAPIDemo1.Model
{
    public class CheckoutDTO
    {
       
            public int SaleId { get; set; }
            public int CustomerId { get; set; }
            public DateTime SaleDate { get; set; }
            public decimal TotalInvoiceAmount { get; set; }
            public decimal Discount { get; set; }
            public string PaymentNaration { get; set; }
            public string DeliveryAddress1 { get; set; }
            public string DeliveryAddress2 { get; set; }
            public string DeliveryCity { get; set; }
            public string DeliveryPinCode { get; set; }
            public string DeliveryLandMark { get; set; }
        }

    
}

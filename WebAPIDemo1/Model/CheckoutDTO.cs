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
            public string PhoneNumber { get; set; }
            public string DiscountCode { get; set; }
            public string DeliveryCity { get; set; }
            public string DeliveryPinCode { get; set; }
            public string FullofName { get; set; }
        }

    
}

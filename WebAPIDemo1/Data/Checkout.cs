using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo1.Data
{
    public class Checkout
    {
        
    [Key]
        public int saleid { get; set; } // Primary Key

        [ForeignKey("Register")]
        public int customerid { get; set; } // Foreign Key

        public DateTime saledate { get; set; }
        public decimal totalinvoiceamount { get; set; }
        public decimal discount { get; set; }
        public string paymentnaration { get; set; }
        public string deliveryaddress1 { get; set; }
        public string deliveryaddress2 { get; set; }
        public string deliverycity { get; set; }
        public string deliverypincode { get; set; }
        public string deliverylandmark { get; set; }

        // Navigation Property (optional if you have a Customer class)
        public  Login Login { get; set; }
    }
}


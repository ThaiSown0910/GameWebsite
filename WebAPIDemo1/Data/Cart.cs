using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDemo1.Data
{
    public class Cart
    {
        [Key]
        public int cartid { get; set; }
        [ForeignKey("Game")]
        public int gameid { get; set; }

        [ForeignKey("Register")]
        public int customerid { get; set; }
        public int quantity { get; set; }
        public DateTime addeddate { get; set; }
        public Login Login { get; set; }
        public Game Game { get; set; }

    }
}

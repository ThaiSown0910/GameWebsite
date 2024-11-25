namespace WebAPIDemo1.Model
{
    public class CartDTO
    {
        public int CartId { get; set; }
        public int GameId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
    }
}

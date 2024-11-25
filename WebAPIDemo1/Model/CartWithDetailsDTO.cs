namespace WebAPIDemo1.Model
{
    public class CartWithDetailsDTO
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        public GameDTO Game { get; set; }
        public RegisterDTO Customer { get; set; }
    }
}

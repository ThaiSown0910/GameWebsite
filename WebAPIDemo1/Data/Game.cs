using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebAPIDemo1.Data
{
    public class Game
    {
        [Key]
        public int gameid { get; set; }

        [Required]
        public string title { get; set; }

        public int year { get; set; }

        public string summary { get; set; }

        public double price { set; get; }
        
        [MaxLength(500)]
        [Url]
        [RegularExpression(@"^https?:\/\/.*\.(jpg|jpeg|png|gif|webp)$", ErrorMessage = "The imageUrl must be a valid HTTP/HTTPS link and end with an image extension (jpg, jpeg, png, gif, webp).")]
        public string imageurl { get; set; }

        [ForeignKey("GameCategory")] // Thiết lập categoryname là khóa ngoại
        public int categoryid { get; set; } // Khóa ngoại tham chiếu đến bảng Movie Category
        public GameCategory GameCategory { get; set; }  // Đối tượng thể loại game
    }
}

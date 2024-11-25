using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPIDemo1.Data
{
    public class GameCategory
    {
        [Key]
        public int categoryid { get; set; }

        [Required]
        public string categoryname { get; set; }

        public string description { get; set; }

        // Liên kết một-nhiều với Game
        public ICollection<Game> Games { get; set; }  // Tập hợp các bộ phim thuộc thể loại này

    }
}

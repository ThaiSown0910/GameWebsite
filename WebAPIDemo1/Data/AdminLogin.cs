using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo1.Data
{
    public class AdminLogin
    {
        [Key]
        [ForeignKey("AdminCreate")]
        public int adminid { get; set; }

        [Required]
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        // Navigation property for Register
        public virtual AdminCreate AdminCreate { get; set; }
    }
}

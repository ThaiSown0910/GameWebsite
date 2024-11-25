using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo1.Data
{
    public class Login
    {
        [Key]
        [ForeignKey("Register")]
        public int customerid { get; set; }

        [Required]
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        // Navigation property for Register
        public virtual Register Register { get; set; }
    }
}

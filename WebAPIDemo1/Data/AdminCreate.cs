using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo1.Data
{
    public class AdminCreate
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int adminid { get; set; }

        [Required]
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password và Confirm Password phải giống nhau.")]
        public string confirmpassword { get; set; }
        [Required]
        public string fullname { get; set; }
        [Required]
        [Phone]
        public string mobilephone { get; set; }
        
            
        // Navigation property for Login
        public virtual AdminLogin AdminLogin { get; set; }
    }
}


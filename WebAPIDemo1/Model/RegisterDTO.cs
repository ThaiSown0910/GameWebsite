    using System.ComponentModel.DataAnnotations;

    namespace WebAPIDemo1.Model
    {
        public class RegisterDTO
        {
   
            public int CustomerId { get; set; }

            public string UserName { get; set; }
      
            public string Password { get; set; }

            public string ConfirmPassword { get; set; }

            public string Email {  get; set; }
        

        }
    }

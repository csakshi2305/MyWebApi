using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApi.Core.Model
{


    [Table("Users")]
    public class UserEmail
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class EmailRequest
    {

        public int Id { get; set; }


    }

    public class EmailResponse
    {

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    
}

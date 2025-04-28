using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Client
{
    public class ClientInfoDTO
    {
        //select [id],[UserNameHash],[PasswordHash],[Email],[FirstName],[LastName],[IsLogging],[IsActive] from Users where [Role]='Employee'
        public int Id { get; set; }
        public string UserNameHash { get; set; }
        public string PasswordHash { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("(/^[A-Za-z]+$/)")]
        public string FirstName { get; set; }
        [RegularExpression("(/^[A-Za-z]+$/)")]
        public string LastName { get; set; }
        public bool IsLogging { get; set; } = false;
        public bool IsActive { get; set; }=true;
        public string FullName { get { return $"{FirstName} {LastName}"; } } 

    }
}

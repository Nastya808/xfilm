

using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9._%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$", ErrorMessage = "Email is not correct!")]
        public string Email { get; set; } = null!;
        [RegularExpression(@"^[a-zA-Z0-9._%±-]{5,}$", ErrorMessage = "Password is not correct")]
        public string Pass { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public bool IsAssert { get; set; }
        public int MaxCountProfile { get; set; }
    }
}

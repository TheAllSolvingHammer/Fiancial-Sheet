using System.ComponentModel.DataAnnotations;

namespace ProektTSPGlaven.Models.Register
{
    public class RegisterModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string passwordRepeat { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProektTSPGlaven.Models.Account
{
    public class AccountModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public decimal balance { get; set; }
    }
}

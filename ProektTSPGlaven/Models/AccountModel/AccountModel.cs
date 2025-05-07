using System.ComponentModel.DataAnnotations;

namespace ProektTSPGlaven.Models.AccountModel
{
    public class AccountModel
    {
        [Required]
        public string name { get; set; }

        [Required]
        public decimal balance { get; set; }
    }
}

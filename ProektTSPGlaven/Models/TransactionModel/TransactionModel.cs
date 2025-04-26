using ProektTSPGlaven.Models.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProektTSPGlaven.Models.TransactionModel
{
    public class TransactionModel
    {
        [Required]
        public decimal amount { get; set; }

        [Required]
        public TransactionType type { get; set; }

        [Required]
        [StringLength(255)]
        public string category { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }
    }
}

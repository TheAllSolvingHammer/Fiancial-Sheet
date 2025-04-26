using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProektTSPGlaven.Models.Database
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int accountID { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal balance { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        public int userID { get; set; }


        [ForeignKey("userID")]
        public User User { get; set; }


        public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
    }
}

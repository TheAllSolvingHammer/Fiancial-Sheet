using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProektTSPGlaven.Models.Database
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }

        [Required]
        [StringLength(200)]
        public string username { get; set; }

        [Required]
        [StringLength(100)]
        public string firstName { get; set; }

        [Required]
        [StringLength(100)]
        public string lastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength (255)]
        public string email { get; set; }

        [Required]
        [StringLength (255)]
        public string hashedPassword { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();


    }
}

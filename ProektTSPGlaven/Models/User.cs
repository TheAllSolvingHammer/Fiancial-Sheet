using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProektTSPGlaven.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userID { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength (255)]
        public string email { get; set; }

        [Required]
        [StringLength (255)]
        public string hashedPassword { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime createdAt { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();


    }
}

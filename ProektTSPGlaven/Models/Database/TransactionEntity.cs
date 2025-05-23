﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProektTSPGlaven.Models.Database
{
    public enum TransactionType
    {
        Income,
        Expense
    }
    public class TransactionEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionID { get; set; }

        public int accountID { get; set; } 
        [ForeignKey("accountID")]
        public Account account { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal amount { get; set; }

        [Required]

        public TransactionType type { get; set; }

        [Required]
        [StringLength(255)]
        public string category { get; set; }


        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; } 
    }
}

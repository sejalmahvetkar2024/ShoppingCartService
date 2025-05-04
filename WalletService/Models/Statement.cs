using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Models
{
    public class Statement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StatementId { get; set; }

        [ForeignKey("WalletId")]
        public long WalletId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }

        public DateTime? TransactionDate { get; set; }

        [ForeignKey("OrderId")]
        public long? OrderId { get; set; }
        [StringLength(200)]
        public string? TransactionRemark { get; set; }
    }

}
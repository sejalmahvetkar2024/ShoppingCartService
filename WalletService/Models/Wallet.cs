using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WalletService.Models
{
    public class Wallet
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long WalletId { get; set; }
        [ForeignKey("OrderId")]
        public long OrderId { get; set; }
        public long UserId { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        //public ICollection<Statement> Statements { get; set; }
    }

}
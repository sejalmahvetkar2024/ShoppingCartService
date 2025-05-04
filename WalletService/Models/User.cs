using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WalletService.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordUser { get; set; }
        [Required]
        [Phone]
        public string MobileNo { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        public DateOnly DateOfBirth { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Role cannot be null")]
        public string Role { get; set; }


        //public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

        //public virtual Merchant? Merchant { get; set; }

        //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
        public ICollection<Statement> Statements { get; set; }
    }

}
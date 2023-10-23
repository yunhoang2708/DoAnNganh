using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteBanGiayDep23.Models.EF
{
    [Table("tb_Wishlist")]
    public class Wishlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Product Product { get; set; }
    }
}
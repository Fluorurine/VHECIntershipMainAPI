using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }

    }
}

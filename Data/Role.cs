using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Data
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(2)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<User> Users { get; set; }

    }
}

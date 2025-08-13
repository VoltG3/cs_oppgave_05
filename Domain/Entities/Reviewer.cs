using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Entities
{
    [Table("reviewer")]
    public class Reviewer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rev_id")]
        public int RevId { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Column("rev_name")]
        public string RevName { get; set; } = string.Empty;
        
        // Relation
        public virtual ICollection<Rating>? Ratings { get; set; }
    }
}

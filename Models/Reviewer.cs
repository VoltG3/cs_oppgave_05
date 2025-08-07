using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("reviewer")]
    public class Reviewer
    {
        [Key]
        [Column("rev_id")]
        public int RevId { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Column("rev_name")]
        public string RevName { get; set; } = string.Empty;
        
    }
}

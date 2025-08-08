using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("actor")]
    public class Actor
    {
        [Key]
        [Column("act_id")]
        public int ActId { get; set; }
        
        [Required]
        [MaxLength(20)]
        [Column("act_fname")]
        public string ActName { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(20)]
        [Column("act_lname")]
        public string ActLname { get; set; } = string.Empty;
        
         [Required]
         [Column("act_gender")]
         [RegularExpression("^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'")]
         public string ActGender { get; set; } = string.Empty;
         
         // Relation
         public virtual ICollection<MovieCast>? MovieCasts { get; set; }
    }
}

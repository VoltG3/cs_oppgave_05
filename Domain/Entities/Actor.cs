using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Entities
{
    [Table("actor")]
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("act_id")]
        public int ActId { get; set; }
        
        [Required]
        [MaxLength(20)]
        [Column("act_fname")]
        public string ActFname { get; set; } = string.Empty;
        
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

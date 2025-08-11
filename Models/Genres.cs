using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("genres")]
    public class Genres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("gen_id")]
        public int GenId { get; set; }
        
        [Required]
        [MaxLength(20)]
        [Column("gen_title")]
        public string GenTitle { get; set; } = string.Empty;
        
        // Relation
        public virtual ICollection<MovieGenres>? MovieGenres { get; set; }
    }
}

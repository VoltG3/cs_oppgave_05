using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("movie_cast")]
    public class MovieCast
    {
        [Column("act_id")]
        public int ActId { get; set; }
        
        [Column("mov_id")]
        public int MovId { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Column("role")]
        public string Role { get; set; } = string.Empty;
        
        // Relation
        [ForeignKey(nameof(ActId))]
        public virtual Actor? Actor { get; set; }
        
        [ForeignKey(nameof(MovId))]
        public virtual Movie? Movie { get; set; }
        
    }
}

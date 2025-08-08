using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("movie_direction")]
    public class MovieDirection
    {
        [Column("dir_id")]
        public int DirId { get; set; }
        
        [Column("mov_id")]
        public int MovId { get; set; }
        
        // Relation
        [ForeignKey(nameof(DirId))]
        public virtual Director? Director { get; set; }

        [ForeignKey(nameof(MovId))]
        public virtual Movie? Movie { get; set; }
    }
}

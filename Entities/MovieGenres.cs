using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Entities
{
    [Table("movie_genres")]
    public class MovieGenres
    {
        [Column("mov_id")]
        public int MovId { get; set; }
        
        [Column("gen_id")]
        public int GenId { get; set; }
        
        // Relation
        [ForeignKey(nameof(MovId))]
        public virtual Movie? Movie { get; set; }
        
        [ForeignKey(nameof(GenId))]
        public virtual Genres? Genres { get; set; }
        
    }
}

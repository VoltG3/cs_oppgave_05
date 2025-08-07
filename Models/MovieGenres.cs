using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("movie_genres")]
    public class MovieGenres
    {
        [Key]
        [Column("mov_id")]
        public int MovId { get; set; }
        
        //[Key]
        [Column("gen_id")]
        public int GenId { get; set; }
    }
}
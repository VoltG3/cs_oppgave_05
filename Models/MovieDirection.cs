using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("movie_direction")]
    public class MovieDirection
    {
        [Key]
        [Column("dir_id")]
        public int DirId { get; set; }

        //[Key]
        [Column("mov_id")]
        public int MovId { get; set; }
    }
}

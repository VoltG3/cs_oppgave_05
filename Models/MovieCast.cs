using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("movie_cast")]
    public class MovieCast
    {
        [Key]
        [Column("act_id")]
        public int MovId { get; set; }
        
        //[Key]
        [Column("dir_id")]
        public int DirId { get; set; }
        
        [Required]
        [MaxLength(30)]
        [Column("role")]
        public string Role { get; set; } = string.Empty;
        
    }
}

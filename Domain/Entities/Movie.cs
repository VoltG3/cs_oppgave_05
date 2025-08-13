using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Entities
{
    [Table("movie")]
    public class Movie
    {
        [Key]
        [Column("mov_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("mov_title")]
        public string MovTitle { get; set; } = string.Empty;

        [Column("mov_year")]
        public int? MovYear { get; set; }

        [Column("mov_time")]
        [Range(1, int.MaxValue, ErrorMessage = "Movie time must be greater than 0")]
        public int? MovTime { get; set; }

        [MaxLength(50)]
        [Column("mov_lang")]
        public string? MovLang { get; set; }

        [Column("mov_dt_rel", TypeName = "date")]
        public DateTime? MovDtRel { get; set; }

        [Required]
        [MaxLength(5)]
        [Column("mov_rel_country")]
        public string MovRelCountry { get; set; } = string.Empty;
        
        // Relation
        public virtual ICollection<MovieDirection>? MovieDirections { get; set; }
        public virtual ICollection<MovieCast>? MovieCasts { get; set; }
        public virtual ICollection<MovieGenres>? MovieGenres { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
        

    }
}

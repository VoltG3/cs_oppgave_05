using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Models
{
    [Table("rating")]
    public class Rating
    {
        [Key]
        [Column("mov_id")]
        public int MovId { get; set; }
        
        //[Key]
        [Column("rev_id")]
        public int RevId { get; set; }

        [Column("rev_stars", TypeName = "decimal(3,1)")]
        public decimal? RevStars { get; set; }

        [Column("num_o_ratings")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of ratings must be 0 or more")]
        public int? NumOfRatings { get; set; }
        
        // RELATIONS
        [ForeignKey(nameof(MovId))]
        public virtual Movie? Movie { get; set; }
        
        [ForeignKey(nameof(RevId))]
        public virtual Reviewer? Reviewer { get; set;}
    }
}
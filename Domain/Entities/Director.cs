using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cs_oppgave_05.Entities

{
    [Table("director")]
    public class Director
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("dir_id")]
        public int DirId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("dir_fname")]
        public string DirFname { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("dir_lname")]
        public string DirLname { get; set; } = string.Empty;
        
        // Relation
        public virtual ICollection<MovieDirection>? MovieDirections { get; set; }
    }
}

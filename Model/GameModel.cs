using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class GameModel
    {
        public virtual int Id { get; set; }
        [StringLength(30), MinLength(2)]
        [Required]
        public virtual string Title { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public virtual DateTime ReleaseDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Range(1, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        [Display(Name = "Price in IDR")]
        public virtual decimal Price { get; set; }
        public virtual int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual GenreModel Genre { get; set; }
    }
}

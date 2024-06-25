using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class GenreModel
    {
        public virtual int Id { get; set; }
        [Display(Name = "Genre")]
        [Required]
        [StringLength(30), MinLength(2)]
        public virtual string GenreName { get; set; }
        [Display(Name = "Only For Adults")]
        public virtual bool AdultOnly { get; set; }
    }
}

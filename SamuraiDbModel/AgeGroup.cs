using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SamuraiDbModel
{
    public class AgeGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AgeName { get; set; }

        [Required]
        public int From { get; set; }

        public int To { get; set; }

        
    }
}

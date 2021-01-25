using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SamuraiDbModel
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<SportCategory> SportCategories { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SamuraiDbModel
{
    public class SportCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public SportCategoryType Type { get; set; }

        public List<Grade> Grades { get; set; } = new List<Grade>();

        public AgeGroup AgeGroup { get; set; }

        public WeightGroup WeightGroup { get; set; }
        public Sex Sex { get; set; }

    }
}

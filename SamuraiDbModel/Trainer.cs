using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamuraiDbModel
{
    public class Trainer:AbstractSportsman
    {
        [Key]
        public int Id { get; set; }

        public SportClub Club { get; set; }
        public List<Sportsman> Sportsmans { get; set; }
    }
}

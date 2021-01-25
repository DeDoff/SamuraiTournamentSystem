using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SamuraiDbModel
{
    public class Sportsman: AbstractSportsman
    {
        [Key]
        public int Id { get; set; }
        public int IKO { get; set; }

        public int SportClubId { get; set; }
        [ForeignKey("SportClubId")]
        public SportClub SportClub { get; set; }

        public int TrainerId { get; set; }
        [ForeignKey("TrainerId")]
        public Trainer Trainer { get; set; }
        public string ImgUrl { get; set; }

    }

    public abstract class AbstractSportsman
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Weight { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Grade Grade { get; set; }
        public Sex Sex { get; set; }
    }
}

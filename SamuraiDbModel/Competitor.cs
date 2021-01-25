using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SamuraiDbModel
{
    public class Competitor : AbstractSportsman
    {
        public int Id { get; set; }


        public int SportsmanId { get; set; }


        public int CompetitionId { get; set; }
        [ForeignKey("CompetitionId")]
        public Competition Competition { get; set; }


        public CompetitionCategory CompetitionCategory { get; set; }

        public bool IsRegistred { get; set; } = false;

        public List<Match> Matches { get; set; }

        public int Level { get; set; }
        public string ClubName {get;set;}
        public string Region { get; set; }
        public string Country { get; set; }

        public int IKO { get; set; }
        public string RegistrationUrl { get; set; }
        public string ImgUrl { get; set; }

        public AgeGroup AgeGroup { get; set; }
        public WeightGroup WeightGroup { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiDbModel.History
{
    public class CompetitionHistory
    {
        [Key]
        public long Id { get; set; }

        public int CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionDatePeriod { get; set; }

        public int MatchId { get; set; }
        public int GridMatchNumber { get; set; }
        public int GlobalMatchNumbe { get; set; }
        public TimeSpan MatchDuration { get; set; }
        public int MatchWinnerId { get; set; }
        public MatchStatus MatchInfo { get; set; }
        public int CompetitionWinnerId { get; set; }

        public int TatamiId { get; set; }

        public int CompetitionCategoryId { get; set; }
        public string CompetitionCategoryName { get; set; }

        public int CompetitorId { get; set; }
        public int SportsmanId { get; set; }
        public int CompetitorLevel { get; set; }
        public string CompetitorSportClubName { get; set; }
        public string CompetitorRegion { get; set; }
        public string CompetitorCountry { get; set; }

        public string CompetitorFirstName { get; set; }
        public string CompetitorLastName { get; set; }
        public decimal CompetitorWeight { get; set; }
        public int CompetitorAge { get; set; }
        public string CompetitorGrade { get; set; }
        public string CompetitorSex { get; set; }
        public string CompetitorAgeGroupName { get; set; }
        public string CompetitorWeightGroupName { get; set; }
    }

}

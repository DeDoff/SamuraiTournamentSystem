﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SamuraiDbModel
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int GridMatchNumber { get; set; }
        public int TatamiMatchNumber { get; set; }

        public int GlobalMatchNumber { get; set; }
        public TimeSpan MatchDuration { get; set; } = TimeSpan.Zero;
        public MatchStatus MatchInfo { get; set; } = MatchStatus.NotStarted;

        public List<Competitor> Competitors { get; set; }

        public int? WinnerId { get; set; }

        public int CompetitionGridId { get; set; }
        [ForeignKey("CompetitionGridId")]
        public CompetitionGrid CompetitionGrid { get; set; }

        public List<Referee> Referees { get; set; }

        public bool IsThirdPlaceMatch { get; set; }

        [ForeignKey("TatamiId")]
        public Tatami Tatami { get; set; }
        public int TatamiId { get; set; }
    }

    public enum MatchStatus
    {
        Active,Finished,NotStarted
    }
}

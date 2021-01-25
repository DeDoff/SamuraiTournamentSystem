using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SamuraiDbModel
{
    public class CompetitionGrid
    {
        [Key]
        public int Id { get; set; }

        public int CompetitionCategoryId { get; set; }
        [ForeignKey("CompetitionCategoryId")]
        public CompetitionCategory CompetitionCategory { get;set;}

        public List<Match> Matches { get; set; }

        public List<CompetitionGridNode> Nodes { get; set; }

        public bool IsFinished { get; set; } = false;
        public short MatchesConunt { get; set; }

        public int WinnerId { get; set; }
        public int TatamiOrder { get; set; }
        public string JsonGrid { get; set; }
    }
}

using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiMatch
    {
        public int id { get; set; }
        public int gridMatchNumber { get; set; }
        public int tatamiMatchNumber { get; set; }
        public int globalMatchNumber { get; set; }
        public TimeSpan matchDuration { get; set; } = TimeSpan.Zero;
        public MatchStatus matchInfo { get; set; } = MatchStatus.NotStarted;

        public int? winnerId { get; set; }

        public int competitionGridId { get; set; }       

        public bool isFirstRoundForCompetitors { get; set; }
        public bool isThirdPlaceMatch { get; set; }

        public int tatamiId { get; set; }
    }
}

using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLogic.Tournaments
{
    public class MatchCreator
    {
        ITournamentSystem tournamentSystem;
        public MatchCreator(ITournamentSystem tournamentSystem)
        {
            this.tournamentSystem = tournamentSystem;
        }
        
        public List<CompetitionGridNode> CreateGrid(IEnumerable<Competitor> competitors, TournamentParameters tournamentParameters) 
        {
            return tournamentSystem.CreateGrid(competitors, tournamentParameters);
        }
    }
}

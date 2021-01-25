using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLogic.Tournaments
{
    public interface ITournamentSystem
    {
        //public List<Match> CreateMatches(IEnumerable<Competitor> competitors, TournamentParameters tournamentParameters);
        public List<CompetitionGridNode> CreateGrid(IEnumerable<Competitor> competitors, TournamentParameters tournamentParameters);
    }
}

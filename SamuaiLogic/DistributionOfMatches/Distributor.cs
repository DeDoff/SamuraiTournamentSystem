using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLogic.DistributionOfMatches
{
    public interface Distributor
    {
        List<Match> Mix(List<Dictionary<int, int>> list, int medium, List<Tatami> tatamis, List<Match> matches);
    }
    public enum DivisionParameter
    {
        DivisionAllowed,
        DivisionNotAllowed
    }
}

using SamuraiDbModel;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiLogic
{
    public class NumberingOfMatches
    {
        public List<CompetitionGridNode> SetMatchesGridNumber(List<CompetitionGridNode> nodes)
        {
            foreach (var group in nodes.GroupBy(x => x.CompetitionGridId).Select(x => new { gridId = x.Key, nodes = x.Select(m => m) }).ToList())
            {
                int i = 1;
                foreach (var gridNode in group.nodes.OrderByDescending(x => x.NodeLevel).ThenBy(x => x.ParentNodeId).ThenBy(x => x.CompetitorRest))
                {
                    gridNode.GridMatchNumber = gridNode.Match.GridMatchNumber = i++;
                }
            }

            return nodes;
        }

        public List<CompetitionGridNode> SetMatchesGlobalNumber(List<CompetitionGridNode> nodes)
        {
            int i = 1;
            foreach (var group in nodes.GroupBy(x => x.Match.Tatami).Select(x => new { tatami = x.Key, nodes = x.Select(m => m) }).ToList())
            {
                foreach (var gridNode in group.nodes.OrderByDescending(x => x.NodeLevel)
                    .ThenBy(x=> x.CompetitionGrid.CompetitionCategory.Name)
                    .ThenBy(x=> x.GridMatchNumber))
                {
                    gridNode.GlobalMatchNumber = gridNode.Match.GlobalMatchNumber = i;//$"{group.tatami.MatchPrefix}{i}";
                    i++;
                }
            }

            return nodes;
        }

        public List<Match> InitTatamiMatchNumber(List<CompetitionGridNode> gridNodes, List<Match> matches, List<Tatami> tatamis)
        {
            List<int> tatamiIds = tatamis.Select(x => x.Id).ToList();
            foreach (var id in tatamiIds)
            {
                int tatamiMatchNumber = 1;
                List<int> gridIds = matches.Where(x => x.TatamiId == id).Select(x => x.CompetitionGridId).Distinct().ToList();
                var tempGridNodes = gridNodes
                                 .Where(x => gridIds.Contains(x.CompetitionGridId) && !x.CompetitorId.HasValue)
                                 .OrderBy(x => x.CompetitionGrid.TatamiOrder)
                                 .ToList();
                int maxNodeLevel = tempGridNodes.Select(x => x.NodeLevel).Max();

                for (int i = maxNodeLevel; i >= 0; i--)
                {
                    var nodes = tempGridNodes.Where(x => x.NodeLevel == i).OrderBy(x => x.CompetitionGridId).ToList();
                    foreach (var n in nodes)
                    {
                        var match = matches.SingleOrDefault(m => m.Id == n.MatchId);
                        match.TatamiMatchNumber = tatamiMatchNumber++;
                    }
                }
            }
            return matches;
        }

    }
}

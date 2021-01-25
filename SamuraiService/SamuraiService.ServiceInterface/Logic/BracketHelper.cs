using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.UiDataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiService.ServiceInterface.Logic
{
    public class BracketHelper
    {
        public UiBracket CreateUiBracket(IEnumerable<CompetitionGridNode> nodes)
        {
            var bracketNodes = CreateNodes(0, nodes.FirstOrDefault(x => x.ParentNode == null), nodes);
            var nodesWithCorrectIds = bracketNodes.OrderBy(x => x.id).ToArray();

            short leavsCount = 0;
            for(short i=0; i< nodesWithCorrectIds.Length; i++)
            {
                var node = nodesWithCorrectIds[i];
                node.id = i;

                if (node.left == null && node.right == null)
                {
                    leavsCount++;
                }
                else
                {
                    var leftWinner = node.left == node.winner;
                    var rightWinner = node.right == node.winner;

                    var leftNode = nodesWithCorrectIds.FirstOrDefault(x=> x.id == node.left);
                    var rightNode = nodesWithCorrectIds.FirstOrDefault(x=> x.id == node.right);

                    rightNode.parentId = leftNode.parentId = node.id;

                    node.left = (short)(node.left.Value - leavsCount*2);
                    node.right = (short)(node.right.Value - leavsCount*2);
                    
                    
                    
                    if (leftWinner)
                    {
                        node.winner = node.left;
                    }
                    if (rightWinner)
                    {
                        node.winner = node.right;
                    }
                }
            }

            return new UiBracket { bracketNodes = nodesWithCorrectIds};
        }

        private List<UiBracketNode> CreateNodes(short currentId, CompetitionGridNode parentNode, IEnumerable<CompetitionGridNode> allNodes)
        {
            List<UiBracketNode> retList = new List<UiBracketNode>();
            var uinode = new UiBracketNode()
            {
                id = currentId,
                competitorId = parentNode.CompetitorId,
                competitorRest = (short)parentNode.CompetitorRest,
                matchId = parentNode.MatchId,
                matchBracketNumber = parentNode.GridMatchNumber,
                matchGlobalNumber = parentNode.GlobalMatchNumber,
                parentId = currentId == 0 ? null : (short)((currentId - 1) / 2),
                winner = parentNode.Winner,
                nodeLevel = parentNode.NodeLevel
            };
            retList.Add(uinode);

            var leftNode = allNodes.FirstOrDefault(x => x.ParentNodeId == parentNode.Id && x.IsLeftNode == true);
            if (leftNode != null)
            {
                uinode.left = (short)(currentId * 2 + 1);
                retList.AddRange(CreateNodes((short)(currentId * 2 + 1), leftNode, allNodes));
            }
            else
            {
                leftNode = parentNode?.ChildrenNodes?.FirstOrDefault(x => x.IsLeftNode == true);
                if (leftNode != null)
                {
                    uinode.left = (short)(currentId * 2 + 1);
                    retList.AddRange(CreateNodes((short)(currentId * 2 + 1), leftNode, allNodes));
                }
            }

            var rightNode = allNodes.FirstOrDefault(x => x.ParentNodeId == parentNode.Id && x.IsLeftNode == false);
            if (rightNode != null)
            {
                uinode.right = (short)(currentId * 2 + 2);
                retList.AddRange(CreateNodes((short)(currentId * 2 + 2), rightNode, allNodes));
            }
            else
            {
                rightNode = parentNode?.ChildrenNodes?.FirstOrDefault(x => x.IsLeftNode == false);
                if (rightNode != null)
                {
                    uinode.right = (short)(currentId * 2 + 2);
                    retList.AddRange(CreateNodes((short)(currentId * 2 + 2), rightNode, allNodes));
                }
            }
            return retList;
        }
    }
}

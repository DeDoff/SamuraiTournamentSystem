using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiBracket
    {
        public UiBracketNode[] bracketNodes { get; set; }
    }

    public class UiBracketNode
    {
        public short id { get; set; }
        public short? parentId { get; set; }
        public short? left { get; set; }
        public short? right { get; set; }
        public short? competitorRest { get; set; }

        public int? competitorId { get; set; }
        public int? winner { get; set; }
        public int? matchId { get; set; }

        public int? matchBracketNumber { get; set; }
        public int? matchGlobalNumber { get; set; }

        public short nodeLevel { get; set; }
    }
}

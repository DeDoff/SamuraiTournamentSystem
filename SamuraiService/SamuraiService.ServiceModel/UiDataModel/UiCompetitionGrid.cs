using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiCompetitionGrid
    {
        public int id { get; set; }
        public int competitionCategoryId { get; set; }

        public bool isFinished { get; set; } = false;
        public short matchesConunt { get; set; }
    }
}

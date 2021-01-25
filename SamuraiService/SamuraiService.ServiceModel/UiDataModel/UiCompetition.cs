using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiCompetition
    {
        public int id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }

        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiCompetitionCategory
    {
        public int id { get; set; }
        public string name { get; set; }

        public int sportCategoryId { get; set; }
                
        public int competitionId { get; set; }
                
        public int tatamiId { get; set; }
        public int weightFrom { get; set; }
        public int weightTo { get; set; }
        public int ageFrom { get; set; }
        public int ageTo { get; set; }
    }
}

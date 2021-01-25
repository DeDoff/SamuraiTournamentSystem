using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiCompetitor:UiAbstractSportsman
    {
        public int id { get; set; }

        public int sportsmanId { get; set; }

        public int competitionId { get; set; }
        
        public bool isRegistred { get; set; } = false;
        public UiAgeGroup ageGroup { get; set; }
        public UiWeightGroup weightGroup { get; set; }
        public int level { get; set; }
        public string clubName { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string imgUrl { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiSportCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public UiAgeGroup ageGroup { get; set; }
        public UiWeightGroup weightGroup { get; set; }
    }
}

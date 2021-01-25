using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiTatami
    {
        public int id { get; set; }
        public string name { get; set; }
        
        public string matchPrefix { get; set; }

        public int competitionId { get; set; }
    }
}

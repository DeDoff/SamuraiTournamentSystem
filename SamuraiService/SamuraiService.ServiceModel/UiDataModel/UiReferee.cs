using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.UiDataModel
{
    public class UiReferee:UiAbstractSportsman
    {
        public int id { get; set; }

        public ReferreeType referreeType { get; set; }

    }

}

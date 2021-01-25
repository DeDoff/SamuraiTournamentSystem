using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.SportsmanService
{
    [Api("Sportsman service", BodyParameter = GenerateBodyParameter.Always, IsRequired = true)]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateSportsman", "POST", Summary = @"Update sportsman",Notes = "Обновление спортсмена")]
    
    public class UpdateSportsman : IReturn<int>
    {
        [ApiMember(Name = "Sportsman",
   Description = "New sportsman information",
   DataType = "UiSportsman",
   ParameterType = "model",
   IsRequired = true)]
           
        public UiSportsman Sportsman { get; set; }
    }
}

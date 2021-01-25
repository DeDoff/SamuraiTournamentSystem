using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.MatchService
{
    [Api("Match service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateMatch", "POST", Summary = @"Update match",
   Notes = "Обновление матча")]
    public class UpdateMatch:IReturn<UiMatch>
    {
        [ApiMember(Name = "Match",
          Description = "Match information",
          DataType = "UiMatch",
          ParameterType = "model",
          IsRequired = true)]
        public UiMatch Match { get; set; }
    }
}

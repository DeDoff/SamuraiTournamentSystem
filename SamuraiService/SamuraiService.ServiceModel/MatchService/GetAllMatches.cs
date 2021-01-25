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
    [Route("/GetAllMatches", "GET", Summary = @"Get all match information",
   Notes = "Поулчение всех матчей")]
    public class GetAllMatches:IReturn<IEnumerable<UiMatch>>
    {
        
    }
}

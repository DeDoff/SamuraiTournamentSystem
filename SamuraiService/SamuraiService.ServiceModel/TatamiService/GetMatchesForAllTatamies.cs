using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.TatamiService
{
    [Api("Tatami service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetMatchesForAllTatamies", "GET", Summary = @"Get matches for all tatamies",
   Notes = "Возвращает все мачти выбранного татами")]
    public class GetMatchesForAllTatamies:IReturn<IEnumerable<UiMatch>>
    {
    }
}

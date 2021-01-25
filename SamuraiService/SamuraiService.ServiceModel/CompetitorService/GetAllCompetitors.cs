using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.CompetitorService
{
    [Api("Competitor service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllCompetitors", "GET", Summary = @"Get all competitors",
      Notes = "Получение всех участников")]
    public class GetAllCompetitors:IReturn<IEnumerable<UiCompetitor>>
    {
    }
}

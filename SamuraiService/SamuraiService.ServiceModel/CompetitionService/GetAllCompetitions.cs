using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.CompetitionService
{
    [Api("Competition service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/GetAllCompetitions", "GET", Summary = @"Get information about all competitions",
    Notes = "Возвращает информацию о всех соревнованиях")]
    public class GetAllCompetitions:IReturn<IEnumerable<UiDataModel.UiCompetition>>
    {

    }
}

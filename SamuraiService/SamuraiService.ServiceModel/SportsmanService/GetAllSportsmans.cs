using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.SportsmanService
{
    [Api("Sportsman service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllSportsmans", "GET", Summary = @"Get all sportsmans",
           Notes = "Получение всех спортсменов")]
    public class GetAllSportsmans:IReturn<IEnumerable<UiDataModel.UiSportsman>>
    {

    }
}

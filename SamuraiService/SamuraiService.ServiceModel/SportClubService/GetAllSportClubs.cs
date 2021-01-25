using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.SportClubService
{
    [Api("SportClub service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllSportClubs", "GET", Summary = @"Get all sport clubs",
              Notes = "Получение всех спортивных клубов")]
    public class GetAllSportClubs:IReturn<IEnumerable<UiDataModel.UiSportClub>>
    {

    }
}

using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.RefereeService
{
    [Api("Referee service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllReferees", "GET", Summary = @"Get all referees",
       Notes = "Получение всех судей")]
    public class GetAllReferees:IReturn<IEnumerable<UiReferee>>
    {
    }
}

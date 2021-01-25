using SamuraiService.ServiceInterface.Extensions;
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
    [Route("/DeleteMatch", "POST", Summary = @"Delete match",
   Notes = "Удаляет матч")]
    public class DeleteMatch:IReturn<ExceptionResponse>
    {
        public int MatchId { get; set; }
    }
}

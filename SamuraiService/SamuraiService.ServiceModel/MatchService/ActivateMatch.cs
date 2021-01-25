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
    [Route("/ActivateMatch", "POST", Summary = @"Activate match",
  Notes = "Активирует матч")]
    public class ActivateMatch:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "MatchId",
         Description = "Match identifier",
         DataType = "int",
         IsRequired = true)]
        public int MatchId { get; set; }
    }
}

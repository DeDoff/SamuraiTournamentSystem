using SamuraiService.ServiceInterface.Extensions;
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
    [Route("/DeleteCompetitor", "POST", Summary = @"Delete competitor",
   Notes = "Удаление участника")]
    public class DeleteCompetitor:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CompetitorId",
           Description = "Competitor identifier",
           DataType = "int",
           IsRequired = true)]
        public int CompetitorId { get; set; }
    }
}

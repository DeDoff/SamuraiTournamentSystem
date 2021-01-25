using SamuraiService.ServiceInterface.Extensions;
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

    [Route("/RemoveCompetitorsFromCompetition", "POST", Summary = @"Remove all competitors with matches from competition",
  Notes = "Удаляет всех участников и матчи из соревнования")]
    public class RemoveCompetitorsFromCompetition:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CompetitionId",
            Description = "Competition identifier",
            DataType = "int",
            IsRequired = true)]
        public int CompetitionId { get; set; }
    }
}

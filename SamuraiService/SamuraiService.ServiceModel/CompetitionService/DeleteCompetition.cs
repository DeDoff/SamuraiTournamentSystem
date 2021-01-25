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

    [Route("/DeleteCompetition", "POST", Summary = @"Delete competition",
    Notes = "Удаление соревнования")]
    public class DeleteCompetition:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CompetitionId",
            Description = "Competition identifier",
            DataType = "int",
            IsRequired = true)]
        public int CompetitionId { get; set; }
    }
}

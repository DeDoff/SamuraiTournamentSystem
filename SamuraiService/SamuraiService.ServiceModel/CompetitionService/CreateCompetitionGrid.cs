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

    [Route("/CreateCompetitionGrid", "POST", Summary = @"Create competition grid",
   Notes = "Создает пулю")]
    public class CreateCompetitionGrid:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CompetitionCategoryId",
           Description = "Competition category identifier",
           DataType = "int",
           IsRequired = true)]
        public int CompetitionCategoryId { get; set; }
    }
}

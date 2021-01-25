using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.CompetitionCategoryService
{
    [Api("CompetitionCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/DeleteCompetitionCategory", "POST", Summary = @"Delete competition category",
      Notes = "Удаление соревнования")]
    public class DeleteCompetitionCategory:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CompetitionCategoryId",
              Description = "Competition category identifier",
              DataType = "int",
              IsRequired = true)]
        public int CompetitionCategoryId { get; set; }
    }
}

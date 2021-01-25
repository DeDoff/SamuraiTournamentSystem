using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.UiDataModel;
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
    [Route("/FinishMatchAndLoadNext", "POST", Summary = @"Finish match and load next",
   Notes = "Завершает матч и возвращает все оставшиеся матчи на татами")]
    public class FinishMatchAndLoadNext:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "FinishMatch",
         Description = "Finish match information",
         DataType = "UiMatch",
         IsRequired = true)]
        public UiMatch FinishMatch { get; set; }
    }
}

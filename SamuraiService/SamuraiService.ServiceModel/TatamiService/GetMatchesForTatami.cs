using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.TatamiService
{
    [Api("Tatami service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetMatchesForTatami", "GET", Summary = @"Get matches for tatami",
   Notes = "Возвращает матчи выьранного татами")]
    public class GetMatchesForTatami:IReturn<IEnumerable<UiMatch>>
    {
        [ApiMember(Name = "TatamiId",
 Description = "Tatami identifier",
 DataType = "int",
 IsRequired = true)]
        public int TatamiId { get; set; }
    }
}

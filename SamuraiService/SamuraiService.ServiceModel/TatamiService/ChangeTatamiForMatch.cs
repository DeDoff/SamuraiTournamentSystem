using SamuraiService.ServiceInterface.Extensions;
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
    [Route("/ChangeTatamiForMatch", "POST", Summary = @"Change tatami for match",
            Notes = "Изменение татами для матча")]
    public class ChangeTatamiForMatch : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "MatchId",
 Description = "Match identifier",
 DataType = "int",
 IsRequired = true)]
        public int MatchId { get; set; }
        [ApiMember(Name = "TatamiId",
 Description = "Tatami identifier",
 DataType = "int",
 IsRequired = true)]
        public int TatmiId { get; set; }
    }
}

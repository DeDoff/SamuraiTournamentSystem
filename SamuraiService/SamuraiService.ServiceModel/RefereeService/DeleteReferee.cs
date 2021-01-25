using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.RefereeService
{
    [Api("Referee service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/DeleteReferee", "POST", Summary = @"Delete referee",
        Notes = "Удаление судьи")]
    public class DeleteReferee : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "RefereeId",
         Description = "Referee identifier",
         DataType = "int",
         IsRequired = true)]
        public int RefereeId { get; set; }
    }
}

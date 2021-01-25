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
    [Route("/DeleteTatami", "POST", Summary = @"Dalete tatami",
            Notes = "Удаление татами")]
    public class DeleteTatami : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "TatamiId",
 Description = "Tatami identifier",
 DataType = "int",
 IsRequired = true)]
        public int TatamiId { get; set; }
    }
}

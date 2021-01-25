using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.SportsmanService
{
    [Api("Sportsman service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/DeleteSportsman", "POST", Summary = @"Delete sportsman",
            Notes = "Удаление спортсмена")]
    public class DeleteSportsman : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "SportsmanId",
   Description = "Sportsman identifier",
   DataType = "int",
   IsRequired = true)]
        public int SportsmanId { get; set; }
    }
}

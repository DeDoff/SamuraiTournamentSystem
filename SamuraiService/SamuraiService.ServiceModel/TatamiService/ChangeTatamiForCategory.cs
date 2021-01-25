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
    [Route("/ChangeTatamiForCategory", "POST", Summary = @"Change tatami for category",
            Notes = "Изменение татами для категории")]
    public class ChangeTatamiForCategory : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CategoryId",
  Description = "Category identifier",
  DataType = "int",
  IsRequired = true)]
        public int CategoryId { get; set; }
        [ApiMember(Name = "TatamiId",
 Description = "Tatami identifier",
 DataType = "int",
 IsRequired = true)]
        public int TatamiId { get; set; }
    }
}

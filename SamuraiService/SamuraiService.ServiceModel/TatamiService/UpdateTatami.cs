using SamuraiService.ServiceInterface.Extensions;
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
    [Route("/UpdateTatami", "POST", Summary = @"Update tatami",
            Notes = "Обновление татами")]
    public class UpdateTatami : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "Tatami",
 Description = "New tatami information",
 DataType = "UiTatami",
 ParameterType = "model",
 IsRequired = true)]
        public UiTatami Tatami { get; set; }
    }
}

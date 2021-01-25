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
    [Route("/CreateTatami", "POST", Summary = @"Create tatami",
            Notes = "Создание татами")]
    public class CreateTatami : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "Tatami",
 Description = "Tatami information",
 DataType = "UiTatami",
 ParameterType = "model",
 IsRequired = true)]
        public UiTatami Tatami { get; set; }
    }
}

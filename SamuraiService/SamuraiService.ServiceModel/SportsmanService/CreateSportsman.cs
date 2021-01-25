using SamuraiService.ServiceModel.UiDataModel;
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
    [Route("/CreateSportsman", "POST", Summary = @"Create sportsman",
             Notes = "Создание спортсмена")]
    public class CreateSportsman : IReturn<int>
    {
        [ApiMember(Name = "Sportsman",
    Description = "Sportsman information",
    DataType = "UiSportsman",
    ParameterType = "model",
    IsRequired = true)]
        public UiSportsman Sportsman { get; set; }
    }
}

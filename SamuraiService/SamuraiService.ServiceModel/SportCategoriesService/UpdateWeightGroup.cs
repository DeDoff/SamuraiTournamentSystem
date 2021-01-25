using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateWeightGroup", "POST", Summary = @"Update weight group",
           Notes = "Обновление весовой группы")]
    public class UpdateWeightGroup : IReturn<int>
    {
        [ApiMember(Name = "WeightGroup",
      Description = "New weight group information",
      DataType = "UiWeightGroup",
      ParameterType = "model",
      IsRequired = true)]
        public UiDataModel.UiWeightGroup WeightGroup { get; set; }
    }
}

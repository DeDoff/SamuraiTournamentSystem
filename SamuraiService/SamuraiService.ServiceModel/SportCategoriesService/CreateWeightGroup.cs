using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateWeightGroup", "POST", Summary = @"Create weight group",
      Notes = "Создание весовой группы")]
    public class CreateWeightGroup : IReturn<int>
    {
        [ApiMember(Name = "WeightGroup",
       Description = "Weight group information",
       DataType = "UiWeightGroup",
       ParameterType = "model",
       IsRequired = true)]
        public UiDataModel.UiWeightGroup WeightGroup { get; set; }
    }
}

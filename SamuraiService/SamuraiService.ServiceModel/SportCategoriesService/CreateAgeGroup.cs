using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateAgeGroup", "POST", Summary = @"Create age group",
      Notes = "Создание возрастной группы")]
    public class CreateAgeGroup : IReturn<int>
    {
        [ApiMember(Name = "AgeGroup",
       Description = "Age group information",
       DataType = "UiAgeGroup",
       ParameterType = "model",
       IsRequired = true)]
        public UiAgeGroup AgeGroup { get; set; }
    }
}

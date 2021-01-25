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
    [Route("/UpdateAgeGroup", "POST", Summary = @"Update age group",
          Notes = "Обновление возрастной группы")]
    public class UpdateAgeGroup : IReturn<int>
    {
        [ApiMember(Name = "AgeGroup",
       Description = "New age group information",
       DataType = "UiAgeGroup",
       ParameterType = "model",
       IsRequired = true)]
        public UiAgeGroup AgeGroup { get; set; }
    }
}

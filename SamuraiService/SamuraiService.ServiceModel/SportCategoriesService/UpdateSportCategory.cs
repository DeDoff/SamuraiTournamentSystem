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
    [Route("/UpdateSportCategory", "POST", Summary = @"Update sport category",
           Notes = "Обновление спортивной категории")]
    public class UpdateSportCategory : IReturn<int>
    {
        [ApiMember(Name = "SportCategory",
       Description = "New sport category information",
       DataType = "UiSportCategory",
       ParameterType = "model",
       IsRequired = true)]
        public UiSportCategory SportCategory { get; set; }
    }
}

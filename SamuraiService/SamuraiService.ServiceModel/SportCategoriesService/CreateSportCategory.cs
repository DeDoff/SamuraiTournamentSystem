using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace SamuraiService.ServiceModel.SportCategoryServiceModel
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateSportCategory", "POST", Summary = @"Create sport category",
      Notes = "Создание спортивной категории")]
    public class CreateSportCategory : IReturn<int>
    {
        [ApiMember(Name = "SportCategory",
       Description = "Sport category information",
       DataType = "UiSportCategory",
       ParameterType = "model",
       IsRequired = true)]
        public UiSportCategory SportCategory { get; set; }
    }
}

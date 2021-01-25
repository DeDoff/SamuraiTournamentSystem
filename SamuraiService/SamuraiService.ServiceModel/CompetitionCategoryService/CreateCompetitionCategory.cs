using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.CompetitionCategoryService
{
    [Api("CompetitionCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    
    [Route("/CreateCompetitionCategory", "POST", Summary = @"Create competition category",
       Notes = "Создает соревновательную категорию")]
    public class CreateCompetitionCategory:IReturn<int>
    {
        [ApiMember(Name = "CompetitionCategory",
            Description = "Competition category information",
            DataType = "UiCompetitionCategory",
            ParameterType = "model",
            IsRequired = true)]
        public UiCompetitionCategory CompetitionCategory { get; set; }
    }
}

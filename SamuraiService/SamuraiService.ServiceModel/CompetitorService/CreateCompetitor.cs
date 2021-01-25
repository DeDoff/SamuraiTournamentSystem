using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.CompetitorService
{
    [Api("Competitor service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateCompetitor", "POST", Summary = @"Create competitor",
    Notes = "Создание участника")]
    public class CreateCompetitor:IReturn<int>
    {
        [ApiMember(Name = "Competitor",
            Description = "Competitor information",
            DataType = "UiCompetitor",
            ParameterType = "model",
            IsRequired = true)]
        public UiCompetitor Competitor { get; set; }
    }
}

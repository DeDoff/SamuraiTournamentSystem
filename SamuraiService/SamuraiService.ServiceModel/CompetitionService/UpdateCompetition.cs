using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.CompetitionService
{
    [Api("Competition service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/UpdateCompetition", "POST", Summary = @"Update competition",
    Notes = "Обновление информации о соревновании")]
    public class UpdateCompetition:IReturn<int>
    {
        [ApiMember(Name = "Competition",
            Description = "New competition information",
            DataType = "UiCompetition",
            ParameterType = "model",
            IsRequired = true)]
        public UiCompetition Competition { get; set; }
    }
}

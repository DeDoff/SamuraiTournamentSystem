using SamuraiDbModel;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.RegistrationService
{

    [Api("Registration service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateCompetitorWithIKO", "POST", Summary = @"Update competitor with IKO card",
      Notes = "Обновление участника соревнования с IKO карточкой")]
    public class UpdateCompetitorWithIKO : IReturn<int>
    {
        [ApiMember(Name = "Competitor",
        Description = "New competitor's information",
        DataType = "UiCompetitor",
        ParameterType = "model",
        IsRequired = true)]
        public UiCompetitor Competitor { get; set; }
    }
}

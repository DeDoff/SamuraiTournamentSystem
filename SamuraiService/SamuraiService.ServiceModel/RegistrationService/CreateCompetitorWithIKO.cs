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
    [Route("/CreateCompetitorWithIKO", "POST", Summary = @"Create competitor with IKO card",
      Notes = "Создание участника соревнования с IKO карточкой")]
    public class CreateCompetitorWithIKO : IReturn<int>
    {
        [ApiMember(Name = "Competitor",
        Description = "Competitor information",
        DataType = "UiCompetitor",
        ParameterType = "model",
        IsRequired = true)]
        public UiCompetitor Competitor { get; set; }
    }
}

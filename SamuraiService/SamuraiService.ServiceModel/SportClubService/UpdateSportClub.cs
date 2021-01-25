using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.SportClubService
{
    [Api("SportClub service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateSportClub", "POST", Summary = @"Update sport club",
            Notes = "Обновление спортивного клуба")]
    public class UpdateSportClub : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "SportClub",
    Description = "New sport club information",
    DataType = "UiSportClub",
    ParameterType = "model",
    IsRequired = true)]
        public UiSportClub SportClub { get; set; }
    }
}

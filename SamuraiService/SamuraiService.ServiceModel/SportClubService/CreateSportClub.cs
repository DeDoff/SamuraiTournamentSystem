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
    [Route("/CreateSportClub", "POST", Summary = @"Create sport club",
             Notes = "Создание спортивного клуба")]
    public class CreateSportClub : IReturn<int>
    {
        [ApiMember(Name = "SportClub",
   Description = "Sport club information",
   DataType = "UiSportClub",
   ParameterType = "model",
   IsRequired = true)]
        public UiSportClub SportClub { get; set; }
    }
}

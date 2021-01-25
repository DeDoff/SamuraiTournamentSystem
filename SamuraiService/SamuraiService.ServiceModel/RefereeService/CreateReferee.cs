using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.RefereeService
{
    [Api("Referee service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateReferee", "POST", Summary = @"Create referee",
          Notes = "Создание судьи")]
    public class CreateReferee : IReturn<int>
    {
        [ApiMember(Name = "Referee",
         Description = "Referee information",
         DataType = "UiReferee",
         ParameterType = "model",
         IsRequired = true)]
        public UiReferee Referee { get; set; }
    }
}

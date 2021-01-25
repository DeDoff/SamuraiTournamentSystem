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
    [Route("/UpdateReferee", "POST", Summary = @"Update referee",
         Notes = "Обновление информации о судье")]
    public class UpdateReferee : IReturn<int>
    {
        [ApiMember(Name = "Referee",
         Description = "New referee's information",
         DataType = "UiReferee",
         ParameterType = "model",
         IsRequired = true)]
        public UiReferee Referee { get; set; }
    }
}

using SamuraiDbModel;
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
    [Route("/CheckingByIKOCard/{IKO}", "GET", Summary = @"Checking of existing competitor with IKO",
        Notes = "Проверка наличия спортсмена с IKO")]
    public class CheckingByIKOCard : IReturn<Competitor>
    {
        [ApiMember(Name = "IKO",
        Description = "IKO card",
        DataType = "int",
        IsRequired = true)]
        public int IKO { get; set; }
    }
}

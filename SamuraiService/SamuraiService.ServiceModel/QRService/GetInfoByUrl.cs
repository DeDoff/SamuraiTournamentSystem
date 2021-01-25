using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.QRService
{
    [Api("QR service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetInfoByUrl", "GET", Summary = @"Get information about competitor by link",
        Notes = "Получение информации об участнике соревнования по ссылке")]
    public class GetInfoByUrl : IReturn<UiDataModel.UiCompetitor>
    {
        [ApiMember(Name = "Url",
          Description = "Registration Url",
          DataType = "string",
          IsRequired = true)]
        public string Url { get; set; }
    }
}

using SamuraiService.ServiceInterface.Extensions;
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
    [Route("/CreateCompetitorsFromCSV", "POST", Summary = @"Create competitors from csv file",
   Notes = "Создает участников соревнования из csv файла")]
    public class CreateCompetitiorsFromCSV:IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "CompetitorsCSV",
           Description = "csv file with competitors",
           DataType = "byte[]",
           ParameterType = "model",
           IsRequired = true)]
        public byte[] CompetitorsCSV { get; set; }
    }
}

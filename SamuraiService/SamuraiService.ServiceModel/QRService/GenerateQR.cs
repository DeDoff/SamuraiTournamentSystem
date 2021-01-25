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
    [Route("/GenerateQR", "POST", Summary = @"Generate QR code",
       Notes = "Обновление матча")]
    public class GenerateQR : IReturn<byte[]>
    {
        [ApiMember(Name = "SportsmanId",
          Description = "Sportsman identifier",
          DataType = "int",
          IsRequired = true)]
        public int SportsmanId { get; set; }
        [ApiMember(Name = "CompetitionId",
          Description = "Competition identifier",
          DataType = "int",
          IsRequired = true)]
        public int CompetitionId { get; set; }
    }
}

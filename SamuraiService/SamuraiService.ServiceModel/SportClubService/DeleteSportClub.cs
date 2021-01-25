using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System.Net;

namespace SamuraiService.ServiceModel.SportClubService
{
    [Api("SportClub service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/DeleteSportClub", "POST", Summary = @"Delete sport club",
           Notes = "Удаление спортивного клуба")]
    public class DeleteSportClub : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "SportClubId",
   Description = "Sport club identifier",
   DataType = "int",
   IsRequired = true)]
        public int SportClubId { get; set; }
    }
}

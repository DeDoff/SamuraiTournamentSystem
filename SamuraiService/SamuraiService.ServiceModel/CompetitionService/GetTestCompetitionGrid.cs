using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System.Net;

namespace SamuraiService.ServiceModel.CompetitionService
{
    [Api("Competition service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/GetTestCompetitionGrid/{CompetitorsCount}", "GET",Summary = @"Get fake competition bracket",
    Notes = "Создает фэйковую пулю по количеству участников")]
    public class GetTestCompetitionGrid : IReturn<UiBracket>
    {
        [ApiMember(Name = "CompetitorsCount", 
            Description = "Count of competitors for braket",
            DataType = "int", 
            IsRequired = true)]
        public int CompetitorsCount { get; set; }
    }
}

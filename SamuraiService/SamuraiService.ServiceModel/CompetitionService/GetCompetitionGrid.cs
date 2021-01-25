using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System.Net;

namespace SamuraiService.ServiceModel.CompetitionService
{
    [Api("Competition service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/GetCompetitionGrid/{CompetitionGridId}", "GET", Summary = @"Get competition braket by id with all nodes",
    Notes = "Получение пули по id")]
    public class GetCompetitionGrid:IReturn<UiBracket>
    {
        [ApiMember(Name = "CompetitionGridId",
            Description = "Competition grid identifier",
            DataType = "int",
            IsRequired = true)]
        public int CompetitionGridId { get; set; }
    }
}

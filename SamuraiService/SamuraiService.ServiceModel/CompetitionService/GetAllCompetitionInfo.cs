using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.CompetitionService
{
    [Api("Competition service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/GetAllCompetitionInfo/{CompetitionId}", "GET", Summary = @"Get competition Information",
    Notes = "Возвращает информацию о соревновании")]
    public class GetAllCompetitionInfo : IReturn<UiDataModel.UiCompetition>
    {
        [ApiMember(Name = "CompetitionId",
            Description = "Competition identifier",
            DataType = "int",
            IsRequired = true)]
        public int CompetitionId { get; set; }
    }
}

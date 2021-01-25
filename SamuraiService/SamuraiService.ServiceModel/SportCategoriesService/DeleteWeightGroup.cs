using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/DeleteWeightGroup", "POST", Summary = @"Delete weight group",
      Notes = "Удаление весовой группы")]
    public class DeleteWeightGroup : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "WeightGroupId",
       Description = "Weight group identifier",
       DataType = "int",
       IsRequired = true)]
        public int WeightGroupId { get; set; }
    }
}

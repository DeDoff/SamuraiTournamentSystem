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
    [Route("/DeleteAgeGroup", "POST", Summary = @"Delete age group",
      Notes = "Удаление возрастной группы")]
    public class DeleteAgeGroup : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "AgeGroupId",
       Description = "Age group identifier",
       DataType = "int",
       IsRequired = true)]
        public int AgeGroupId { get; set; }
    }
}

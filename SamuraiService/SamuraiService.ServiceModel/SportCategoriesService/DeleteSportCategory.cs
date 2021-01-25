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
    [Route("/DeleteSportCategory", "POST", Summary = @"Delete sport category",
      Notes = "Удаление спортивной категории")]
    public class DeleteSportCategory : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "SportCategoryId",
       Description = "Sport category identifier",
       DataType = "int",
       IsRequired = true)]
        public int SportCategoryId { get; set; }
    }
}

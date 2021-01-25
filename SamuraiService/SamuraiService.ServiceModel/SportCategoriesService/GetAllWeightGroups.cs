using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllWeightGroups", "GET", Summary = @"Get all weight groups",
           Notes = "Получение всех весовых групп")]
    public class GetAllWeightGroups:IReturn<IEnumerable<UiDataModel.UiWeightGroup>>
    {
    }
}

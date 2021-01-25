using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllAgeGroups", "GET", Summary = @"Get all age groups",
           Notes = "Получение всех возрастных групп")]
    public class GetAllAgeGroups:IReturn<IEnumerable<UiDataModel.UiAgeGroup>>
    {
    }
}

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.SportCategoriesService
{
    [Api("SportCategory service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllSportCategories", "GET", Summary = @"Get all sport categories",
           Notes = "Получение всех спортивных категорий")]
    public class GetAllSportCategories : IReturn<IEnumerable<UiDataModel.UiSportCategory>>
    {

    }
}

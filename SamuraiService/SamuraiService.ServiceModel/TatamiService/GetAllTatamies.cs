using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceModel.TatamiService
{
    [Api("Tatami service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllTatamies", "GET", Summary = @"Get all tatamies",
                 Notes = "Получение всех татами")]
    public class GetAllTatamies:IReturn<IEnumerable<UiTatami>>
    {
    }
}

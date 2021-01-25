using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System.Collections.Generic;
using System.Net;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/GetAllRoles", "GET", Summary = @"Get all user roles",
               Notes = "Получение всех ролей пользователя")]
    public class GetAllRoles:IReturn<IEnumerable<UiRole>>
    {
    }
}

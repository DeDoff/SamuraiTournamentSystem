using ServiceStack;
using System.Collections.Generic;
using System.Net;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]

    [Route("/GetAllUsers/", "GET", Summary = @"Get all users with roles",
        Notes = "Получение всех пользователей")]
    public class GetAllUsers:IReturn<IEnumerable<UiDataModel.UiUser>>
    {
    }
}

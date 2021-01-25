using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System.Net;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateUserRole", "POST", Summary = @"Create user role",
            Notes = "Создание роли пользователя")]
    public class CreateUserRole : IReturn<int>
    {
        [ApiMember(Name = "Role",
Description = "Role information",
DataType = "UiRole",
IsRequired = true)]
        public UiRole Role { get; set; }
    }
}

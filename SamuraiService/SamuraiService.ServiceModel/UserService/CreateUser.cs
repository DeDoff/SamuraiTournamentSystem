using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System.Net;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/CreateUser", "POST", Summary = @"Create user",
            Notes = "Создание пользователя")]
    public class CreateUser : IReturn<int>
    {
        [ApiMember(Name = "User",
Description = "User information",
DataType = "UiUser",
IsRequired = true)]
        public UiUser User { get; set; }
    }
}

using SamuraiDbModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateUser", "POST", Summary = @"Update user",
            Notes = "Обновление пользователя")]
    public class UpdateUser : IReturn<int>
    {
        [ApiMember(Name = "User",
Description = "New user information",
DataType = "UiUser",
IsRequired = true)]
        public UiDataModel.UiUser User { get; set; }
    }
}

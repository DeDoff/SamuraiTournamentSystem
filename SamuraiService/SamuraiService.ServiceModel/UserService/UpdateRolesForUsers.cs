using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/UpdateRolesForUser", "POST", Summary = @"Update roles for user",
           Notes = "Обновление ролей для пользователя")]
    public class UpdateRolesForUsers : IReturnVoid
    {
        [ApiMember(Name = "UserRolesIds",
Description = "User roles ids",
DataType = "IEnumerable<int>",
IsRequired = true)]
        public IEnumerable<int> UserRolesIds { get; set; }
        [ApiMember(Name = "UserIds",
Description = "User's ids",
DataType = "IEnumerable<int>",
IsRequired = true)]
        public IEnumerable<int> UserIds { get; set; }
    }
}

using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SamuraiService.ServiceModel.UserServiceModel
{
    [Api("User service")]
    [ApiResponse(HttpStatusCode.InternalServerError, "Oops, something broke")]
    [Route("/DeleteUserRole", "POST", Summary = @"Delete user role",
            Notes = "Уделение роли пользователя")]
    public class DeleteUserRole : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "RoleId",
Description = "Role identifier",
DataType = "int",
IsRequired = true)]
        public int RoleId { get; set; }
    }
}

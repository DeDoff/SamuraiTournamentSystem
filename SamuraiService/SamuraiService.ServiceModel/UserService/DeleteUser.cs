
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
    [Route("/DeleteUser", "POST", Summary = @"Delete user",
           Notes = "Удаление пользователя")]
    public class DeleteUser : IReturn<ExceptionResponse>
    {
        [ApiMember(Name = "UserId",
Description = "User identifier",
DataType = "int",
IsRequired = true)]
        public int UserId { get; set; }
    }
}

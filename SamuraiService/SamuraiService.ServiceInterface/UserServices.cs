using ServiceStack;
using SamuraiService.ServiceModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SamuraiDbModel;
using SamuraiService.ServiceModel.UserServiceModel;
using System.Collections.Generic;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.UiDataModel;
using System;

namespace SamuraiService.ServiceInterface
{
    public class UserServices : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateUser request)
        {
            if (request.User == null)
                return -1;

            var user = request.User.ConvertObject<UiUser, User>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                user.CreatedDate = System.DateTime.Now;
                user.PasswordHash = request.User.password.ToMD5();

                if (request.User.selectedRoles != null && request.User.selectedRoles.Any())
                {
                    var rolesIds = request.User.selectedRoles.Select(x => x.id).ToList();
                    user.Roles = db.UserRoles.Where(x => rolesIds.Contains(x.Id)).ToList();
                }
                
                db.Users.Add(user);
                db.SaveChanges();
                return user.Id;
            }
        }

        public object Any(GetAllUsers request)
        {
            List<User> users;
            using (var db = dbContextFactory.CreateDbContext())
            {
                users = db.Users.Include(x => x.Roles).AsNoTracking().ToList();
            }

            return users.ToUiModel();
        }

        public object Any(UpdateUser request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var user = db.Users.Include(x=> x.Roles).SingleOrDefault(x => x.Id == request.User.id);
                if (user != null)
                {
                    foreach (var outputObjectFiled in user.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.User.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.User) != outputObjectFiled.GetValue(user))
                                {
                                    outputObjectFiled.SetValue(user, inputObjectField.GetValue(request.User));
                                }
                            }
                        }
                    }
                    if (request.User.password != null)
                    {
                        var newPassword = request.User.password.ToMD5();
                        if (newPassword != user.PasswordHash)
                            user.PasswordHash = newPassword;
                    }

                    //if (request.User.selectedRoles != null && request.User.selectedRoles.Any())
                    //{
                    //    var rolesIds = request.User.selectedRoles.Select(x => x.id).ToList();
                    //    user.Roles = db.UserRoles.Where(x => rolesIds.Contains(x.Id)).ToList();
                    //}
                    //else
                    //{
                    //    user.Roles = new List<UserRole>();
                    //}

                    db.Users.Update(user);
                    db.SaveChanges();
                }

                return user.Id;
            }
        }

        public object Any(DeleteUser request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var user = db.Users.SingleOrDefault(x => x.Id == request.UserId);
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        response.IsSuccess = true;     
                    }
                }catch(Exception e)
                {
                    response.IsSuccess = false;
                    response.ExceptionMessage = e.Message;
                }
                return response;
            }
        }

        public object Any(CreateUserRole request)
        {
            if (request.Role == null)
                return -1;

            var role = request.Role.ConvertObject<UiRole, UserRole>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                db.UserRoles.Add(role);
                db.SaveChanges();
                return request.Role;
            }
        }

        public object Any(GetAllRoles request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                return db.UserRoles.AsNoTracking().ToList().ConvertObjects<UserRole,UiRole>();
            }
        }

        public object Any(DeleteUserRole request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var role = db.UserRoles.SingleOrDefault(x => x.Id == request.RoleId);
                    if (role != null)
                    {
                        db.UserRoles.Remove(role);
                        db.SaveChanges();
                        response.IsSuccess = true;
                    }
                }catch(Exception e)
                {
                    response.IsSuccess = false;
                    response.ExceptionMessage = e.Message;
                }
                return response;
            }
        }

        public void Any(UpdateRolesForUsers request)
        {
            if (request.UserIds == null || !request.UserIds.Any())
                return;

            using (var db = dbContextFactory.CreateDbContext())
            {
                foreach (var user in db.Users.Where(x => request.UserIds.Contains(x.Id)).ToList())
                {
                    if (request.UserRolesIds == null || !request.UserRolesIds.Any())
                        user.Roles.Clear();
                    else
                        user.Roles = db.UserRoles.Where(x => request.UserRolesIds.Contains(x.Id)).ToList();
                }

                db.SaveChanges();
            }

        }
    }
}

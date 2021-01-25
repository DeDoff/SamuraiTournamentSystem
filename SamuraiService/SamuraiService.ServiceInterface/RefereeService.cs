using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.RefereeService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class RefereeService:Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateReferee request)
        {
            if (request.Referee == null)
                return -1;

            var referee = request.Referee.ConvertObject<UiReferee, Referee>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                db.Add(referee);
                db.SaveChanges();

                return referee.Id;
            }
        }

        public object Any(DeleteReferee request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var referee = db.Referees.SingleOrDefault(x => x.Id == request.RefereeId);
                    if (referee != null)
                    {
                        db.Referees.Remove(referee);
                        db.SaveChanges();
                        response.IsSuccess = true;
                    }
                }catch(Exception e)
                {
                    response.ExceptionMessage = e.Message;
                    response.IsSuccess = false;
                }
                return response;
            }
        }

        public object Any(GetAllReferees request)
        {
            List<Referee> referees;
            using (var db = dbContextFactory.CreateDbContext())
            {
                referees = db.Referees.AsNoTracking().ToList();
            }
            return referees.ConvertObjects<Referee, UiReferee>().ToList();
        }

        public int Any(UpdateReferee request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var referee = db.Referees.SingleOrDefault(x => x.Id == request.Referee.id);
                if (referee != null)
                {
                    foreach (var outputObjectFiled in referee.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.Referee.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.Referee) != outputObjectFiled.GetValue(referee))
                                {
                                    outputObjectFiled.SetValue(referee, inputObjectField.GetValue(request.Referee));
                                }
                            }
                        }
                    }
                                      
                    db.Referees.Update(referee);
                    db.SaveChanges();
                }
                return referee.Id;
            }
        }
    }
}

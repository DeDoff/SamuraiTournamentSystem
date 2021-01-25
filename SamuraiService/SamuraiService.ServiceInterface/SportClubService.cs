using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.SportClubService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class SportClubService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateSportClub request)
        {
            if (request.SportClub == null)
                return -1;

            var sportClub = request.SportClub.ConvertObject<UiSportClub, SportClub>();

            using (var db = dbContextFactory.CreateDbContext())
            {
                db.SportClubs.Add(sportClub);
                db.SaveChanges();
                return sportClub.Id;
            }
        }

        public object Any(GetAllSportClubs request)
        {
            List<SportClub> sportClubs;
            using (var db = dbContextFactory.CreateDbContext())
            {
                sportClubs = db.SportClubs.AsNoTracking().ToList();
            }
            return sportClubs.ConvertObjects<SportClub, UiSportClub>().ToList();
        }

        public object Any(DeleteSportClub request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var sportClub = db.SportClubs.SingleOrDefault(x => x.Id == request.SportClubId);
                    if (sportClub != null)
                    {
                        db.SportClubs.Remove(sportClub);
                        db.SaveChanges();
                        response.IsSuccess = true;
                    }
                }
                catch (Exception e)
                {
                    response.IsSuccess = false;
                    response.ExceptionMessage = e.Message;
                }

                return response;
            }
        }

        public object Any(UpdateSportClub request)
        {
            ExceptionResponse response = new ExceptionResponse();
            using (var db = dbContextFactory.CreateDbContext())
            {
                try
                {
                    var club = db.SportClubs.SingleOrDefault(x => x.Id == request.SportClub.id);
                    if (club == null) throw new ArgumentNullException($"Sport club with id = {request.SportClub.id} doesn't exist");

                    foreach (var outputObjectFiled in club.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.SportClub.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.SportClub) != outputObjectFiled.GetValue(club))
                                {
                                    outputObjectFiled.SetValue(club, inputObjectField.GetValue(request.SportClub));
                                }
                            }
                        }
                    }

                    db.SportClubs.Update(club);
                    db.SaveChanges();

                    response.IsSuccess = true;
                }
                catch (Exception e)
                {
                    response.IsSuccess = false;
                    response.ExceptionMessage = e.Message;
                }
                return response;
            }
        }
    }
}

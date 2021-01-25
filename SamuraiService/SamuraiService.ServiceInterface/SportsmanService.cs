using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.SportsmanService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class SportsmanService:Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateSportsman request)
        {
            if (request.Sportsman == null)
                return -1;

            var sportsman = request.Sportsman.ConvertObject<UiSportsman, Sportsman>();
            
            using (var db = dbContextFactory.CreateDbContext())
            {
                sportsman.DateOfBirth = StringExtention.ToDateTimeFromUiString(request.Sportsman.birthDate);
                sportsman.Weight = request.Sportsman.weight;
                sportsman.Sex = db.Sexes.FirstOrDefault(x=>x.Name.ToLower()==request.Sportsman.sex.ToLower());
                sportsman.Grade = db.Grades.FirstOrDefault(x => x.Name.ToLower() == request.Sportsman.grade.ToLower());
                sportsman.SportClub = db.SportClubs.SingleOrDefault(x => x.Name.ToLower() == request.Sportsman.club.ToLower());
                sportsman.Trainer = db.Trainers.SingleOrDefault(x => x.FirstName.ToLower() +" "+ x.LastName.ToLower() == request.Sportsman.trainer.ToLower());
                db.Sportsmens.Add(sportsman);
                db.SaveChanges();
                return sportsman.Id;
            }
        }

        public object Any(GetAllSportsmans request)
        {
            List<Sportsman> sportsmens;
            List<UiSportsman> uiSportsmen = new List<UiSportsman>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                sportsmens = db.Sportsmens
                                .Include(x => x.SportClub)
                                .Include(x => x.Sex)
                                .Include(x => x.Grade)
                                .Include(x=>x.Trainer)
                                .AsNoTracking().ToList();
                foreach(var sportsman in sportsmens)
                {
                    UiSportsman uiSportsman = sportsman.ConvertObject<Sportsman, UiSportsman>();
                    var ageGroup = db.AgeGroups.FirstOrDefault(a => (sportsman.Age >= a.From && sportsman.Age <= a.To && a.To != 0) || (sportsman.Age >= a.From && a.To == 0));
                    var weightGroup = db.WeightGroups.FirstOrDefault(a => (sportsman.Weight >= a.From && sportsman.Weight <= a.To && a.To != 0) || (sportsman.Weight >= a.From && a.To == 0));
                    uiSportsman.birthDate = StringExtention.ToUiString(sportsman.DateOfBirth);
                    uiSportsman.club = sportsman.SportClub.Name;
                    uiSportsman.trainer = sportsman.Trainer.FirstName + " " + sportsman.Trainer.LastName;
                    uiSportsman.weight = (int)sportsman.Weight;
                    uiSportsman.grade = sportsman.Grade.Name;

                    uiSportsmen.Add(uiSportsman);
                }
                
            }
            return uiSportsmen;
        }

        public object Any(DeleteSportsman request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var sportsman = db.Sportsmens.SingleOrDefault(x => x.Id == request.SportsmanId);
                    if (sportsman != null)
                    {
                        db.Sportsmens.Remove(sportsman);
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

        public object Any(UpdateSportsman request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var sportsman = db.Sportsmens.SingleOrDefault(x => x.Id == request.Sportsman.id);
                if (sportsman != null)
                {
                    foreach (var outputObjectFiled in sportsman.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.Sportsman.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.Sportsman) != outputObjectFiled.GetValue(sportsman))
                                {
                                    outputObjectFiled.SetValue(sportsman, inputObjectField.GetValue(request.Sportsman));
                                }
                            }
                        }
                    }
                    sportsman.Grade = db.Grades.SingleOrDefault(x => x.Name.ToLower() == request.Sportsman.grade.ToLower());
                    sportsman.Trainer = db.Trainers.SingleOrDefault(x => x.FirstName.ToLower() + " " + x.LastName.ToLower() == request.Sportsman.trainer.ToLower());
                    sportsman.Sex = db.Sexes.SingleOrDefault(x => x.Name.ToLower() == request.Sportsman.sex.ToLower());
                    sportsman.SportClub = db.SportClubs.SingleOrDefault(x => x.Name.ToLower() == request.Sportsman.club.ToLower());
                    sportsman.DateOfBirth = StringExtention.ToDateTimeFromUiString(request.Sportsman.birthDate);

                    db.Sportsmens.Update(sportsman);
                    db.SaveChanges();
                }
                return sportsman.Id;
            }
        }

    }
}

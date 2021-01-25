using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.RegistrationService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class RegistrationService:Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public object Any(CheckingByIKOCard request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var competitor = db.Competitors
                                    .Include(c=>c.AgeGroup)
                                    .Include(c=>c.WeightGroup)
                                    .Include(c=>c.Grade)
                                    .Include(c=>c.Sex)
                                    .Where(c => c.IKO == request.IKO)
                                    .First();
                if (competitor == null)
                {
                    return new UiCompetitor();
                }
                else
                {
                    UiCompetitor c = new UiCompetitor();
                    c = competitor.ConvertObject<Competitor, UiCompetitor>();
                    c.ageGroup = competitor.AgeGroup.ConvertObject<AgeGroup,UiAgeGroup>();
                    c.weightGroup = competitor.WeightGroup.ConvertObject<WeightGroup, UiWeightGroup>();
                    c.grade = competitor.Grade.Name;
                    c.weightGroup.sex = competitor.Sex.Name;
                    return c;
                }
            }
        }

        public int Any(CreateCompetitorWithIKO request)
        {
            if (request.Competitor == null)
                return -1;

            var competitor = request.Competitor.ConvertObject<UiCompetitor, Competitor>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                //соревновательная категория
                var sportsmen = db.Sportsmens
                                    .Include(x => x.Sex)
                                    .Include(x => x.Grade)
                                    .FirstOrDefault(x => x.Id == competitor.SportsmanId);
                competitor.Grade = sportsmen.Grade;
                competitor.WeightGroup = db.WeightGroups.FirstOrDefault(a => (sportsmen.Weight >= a.From && sportsmen.Weight <= a.To && a.To != 0) || (sportsmen.Weight >= a.From && a.To == 0));
                competitor.AgeGroup = db.AgeGroups.FirstOrDefault(a => (sportsmen.Age >= a.From && sportsmen.Age <= a.To && a.To != 0) || (sportsmen.Age >= a.From && a.To == 0));
                competitor.Sex = sportsmen.Sex;
                
                var sportCategoryId = db.SportCategories
                                                .FirstOrDefault(y => y.AgeGroup.Id == competitor.AgeGroup.Id
                                                && y.Sex.Id == competitor.Sex.Id
                                                && y.WeightGroup.Id == competitor.WeightGroup.Id).Id;
                competitor.CompetitionCategory = db.CompetitionCategories.Where(s => s.SportCategoryId == sportCategoryId).FirstOrDefault();
                db.Competitors.Add(competitor);
                db.SaveChanges();

                return competitor.Id;
            }
        }

        public int Any(UpdateCompetitorWithIKO request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var competitor = db.Competitors
                                    .Include(c => c.AgeGroup)
                                    .Include(c => c.WeightGroup)
                                    .Include(c => c.Grade)
                                    .Include(c => c.Sex)
                                    .Where(c => c.IKO == request.Competitor.iko)
                                    .First();
                if (competitor.Age != request.Competitor.age)
                {
                    competitor.Age = request.Competitor.age;
                    competitor.AgeGroup = db.AgeGroups.FirstOrDefault(a => (competitor.Age >= a.From && competitor.Age <= a.To && a.To != 0) || (competitor.Age >= a.From && a.To == 0));
                }
                if (competitor.Weight != request.Competitor.weight)
                {
                    competitor.Weight = request.Competitor.weight;
                    competitor.WeightGroup = db.WeightGroups.FirstOrDefault(a => (competitor.Weight >= a.From && competitor.Weight <= a.To && a.To != 0) || (competitor.Weight >= a.From && a.To == 0));       
                }
                if (competitor.Grade.Name.ToLower() != request.Competitor.grade.ToLower())
                {
                    competitor.Grade = db.Grades.Where(g => g.Name.ToLower() == request.Competitor.grade.ToLower()).FirstOrDefault();
                }
                if (competitor.ClubName.ToLower() != request.Competitor.clubName.ToLower())
                {
                    competitor.ClubName = request.Competitor.clubName;
                }
                return competitor.Id;
            }
        }

    }
}

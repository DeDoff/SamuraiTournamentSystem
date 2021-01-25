using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceInterface.Logic;
using SamuraiService.ServiceModel.CompetitorService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class CompetitorService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateCompetitor request)
        {
            if (request.Competitor == null)
                return -1;

            var competitor = request.Competitor.ConvertObject<UiCompetitor, Competitor>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                //соревновательная категория
                var sportsmen = db.Sportsmens
                        .Include(x=>x.Sex)
                        .Include(x=>x.Grade)
                        .FirstOrDefault(x => x.Id == competitor.SportsmanId);
                competitor.Competition = db.Competitions.SingleOrDefault(x => x.Id == request.Competitor.competitionId);
                competitor.Grade = sportsmen.Grade;
                competitor.AgeGroup = db.AgeGroups.FirstOrDefault(a => (sportsmen.Age >= a.From && sportsmen.Age <= a.To && a.To != 0) || (sportsmen.Age >= a.From && a.To == 0));
                competitor.WeightGroup = db.WeightGroups.FirstOrDefault(a => (sportsmen.Weight >= a.From && sportsmen.Weight <= a.To && a.To != 0) || (sportsmen.Weight >= a.From && a.To == 0));
                competitor.Sex = sportsmen.Sex;
                var competitorSportCategory = db.SportCategories
                            .FirstOrDefault(y => y.AgeGroup.Id == competitor.AgeGroup.Id
                                                && y.Sex.Id == competitor.Sex.Id
                                                && y.WeightGroup.Id == competitor.WeightGroup.Id);

                var competitionCategory = db.CompetitionCategories
                    .Include(x=>x.Competitors)                            
                    .Where(y => y.SportCategoryId == competitorSportCategory.Id).FirstOrDefault();
                competitor.CompetitionCategory = competitionCategory;
                competitionCategory.Competitors.Add(competitor);
                db.Competitors.Add(competitor);
                db.CompetitionCategories.Update(competitionCategory);
                db.SaveChanges();

                return competitor.Id;
            }
        }

        public object Any(DeleteCompetitor request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var competitor = db.Competitors.SingleOrDefault(x => x.Id == request.CompetitorId);
                    if (competitor != null)
                    {
                        db.Competitors.Remove(competitor);
                        db.SaveChanges();
                        response.IsSuccess = true;
                    }
                }
                catch (Exception e)
                {
                    response.ExceptionMessage = e.Message;
                    response.IsSuccess = false;
                }
                return response;
            }
        }

        public object Any(GetAllCompetitors request)
        {
            List<UiCompetitor> uiCompetitors;
            List<Competitor> competitors;
            using (var db = dbContextFactory.CreateDbContext())
            {
                competitors = db.Competitors
                                .Include(x=>x.WeightGroup)
                                .Include(x=>x.AgeGroup)
                                .Include(x=>x.Sex)
                                .Include(x=>x.Grade)
                                .AsNoTracking()
                                .ToList();

                uiCompetitors = competitors.ConvertObjects<Competitor, UiCompetitor>().ToList();

                for(int i = 0; i < competitors.Count; i++)
                {
                    uiCompetitors[i].ageGroup = competitors[i].AgeGroup.ConvertObject<AgeGroup, UiAgeGroup>();
                    uiCompetitors[i].weightGroup = competitors[i].WeightGroup.ConvertObject<WeightGroup, UiWeightGroup>();
                    uiCompetitors[i].weightGroup.sex = competitors[i].Sex.Name;
                    uiCompetitors[i].grade = competitors[i].Grade.Name;
                }
            }
            return uiCompetitors;
        }

        public int Any(UpdateCompetitor request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var competitor = db.Competitors.SingleOrDefault(x => x.Id == request.Competitor.id);
                if (competitor != null)
                {
                    foreach (var outputObjectFiled in competitor.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.Competitor.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.Competitor) != outputObjectFiled.GetValue(competitor))
                                {
                                    outputObjectFiled.SetValue(competitor, inputObjectField.GetValue(request.Competitor));
                                }
                            }
                        }
                    }

                    db.Competitors.Update(competitor);
                    db.SaveChanges();
                }
                return competitor.Id;
            }
        }

        public object Any(CreateCompetitiorsFromCSV request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                //string competitorsString = "CompetitionId,ClubName,Region,Country,IKO,FirstName,LastName,Weight,Age,Sex,Grade\n"+
                //                        "1,,Brestskaya obl, Belarus,123,Andrey,Kolen,123,20,М,1 Дан\n"+
                //                        "1,asdassss,,Russiaa,111,sdf,Aliyd,100,21,М,6 Дан\n"+
                //                        "1,aaaaaaaa,,,321,aaaaaa,qwerq,144,23,M,8 Дан";

                //byte[] bytes = Encoding.UTF8.GetBytes(competitorsString);

                //CompetitorCSVParser.Parse(bytes);
                List<Competitor> competitors = CompetitorCSVParser.Parse(request.CompetitorsCSV);
                
                response.IsSuccess = FillDBCompetitorsFromCSV.FillDB(competitors);
            }
            catch (FormatException e)
            {
                if (e.Message == "Empty field")
                {
                    response.ExceptionMessage = e.Message;
                }
                else response.ExceptionMessage = "String instead of number";
                response.IsSuccess = false;
            }
            catch (IndexOutOfRangeException e)
            {
                response.IsSuccess = false;
                response.ExceptionMessage = e.Message;
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

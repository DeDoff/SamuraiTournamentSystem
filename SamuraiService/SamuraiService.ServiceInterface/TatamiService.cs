using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiLogic;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.TatamiService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class TatamiService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public object Any(CreateTatami request)
        {
            if (request.Tatami == null)
                return -1;

            var tatami = request.Tatami.ConvertObject<UiTatami, Tatami>();
            ExceptionResponse response = new ExceptionResponse();
            using (var db = dbContextFactory.CreateDbContext())
            {
                try
                {
                    if (db.Competitions.SingleOrDefault(c => c.Id == request.Tatami.competitionId) == null) 
                        throw new ArgumentNullException($"Competition with id = {request.Tatami.competitionId} doesn't exist");

                    db.Tatamis.Add(tatami);
                    db.SaveChanges();
                    response.IsSuccess = true;
                }catch(Exception e)
                {
                    response.IsSuccess = false;
                    response.ExceptionMessage = e.Message;
                }
                return response;
            }
        }

        public object Any(GetAllTatamies request)
        {
            List<Tatami> tatamies;
            using (var db = dbContextFactory.CreateDbContext())
            {
                tatamies = db.Tatamis.AsNoTracking().ToList();
            }
            return tatamies.ConvertObjects<Tatami, UiTatami>().ToList();
        }

        public object Any(DeleteTatami request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var tatami = db.Tatamis.SingleOrDefault(x => x.Id == request.TatamiId);
                    if (tatami != null)
                    {
                        db.Tatamis.Remove(tatami);
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

        public object Any(UpdateTatami request)
        {
            ExceptionResponse response = new ExceptionResponse();
            using (var db = dbContextFactory.CreateDbContext())
            {
                try
                {
                    var tatami = db.Tatamis.SingleOrDefault(x => x.Id == request.Tatami.id);

                    if (tatami == null) throw new ArgumentNullException("Tatami doesn't exist");
                    if (db.Competitions.SingleOrDefault(c => c.Id == request.Tatami.competitionId) == null) 
                        throw new ArgumentNullException($"Competition with id = {request.Tatami.competitionId} doesn't exist");

                    foreach (var outputObjectFiled in tatami.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.Tatami.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.Tatami) != outputObjectFiled.GetValue(tatami))
                                {
                                    outputObjectFiled.SetValue(tatami, inputObjectField.GetValue(request.Tatami));
                                }
                            }
                        }
                    }

                    db.Tatamis.Update(tatami);
                    db.SaveChanges();
                    response.IsSuccess = true;
                }
                catch (Exception e)
                {
                    response.ExceptionMessage = e.Message;
                    response.IsSuccess = false;
                }
                return response;
            }
        }

        public object Any(ChangeTatamiForMatch request)
        {
            ExceptionResponse response = new ExceptionResponse();
            using (var db = dbContextFactory.CreateDbContext())
            {
                try
                {
                    var match = db.Matches.FirstOrDefault(x => x.Id == request.MatchId);
                    var tatami = db.Tatamis.FirstOrDefault(x => x.Id == request.TatmiId);

                    if (match == null) throw new ArgumentNullException("Match doesn't exist");
                    if (tatami == null) throw new ArgumentNullException("Tatami doesn't exist");

                    match.Tatami = tatami;

                    db.Matches.Update(match);
                    db.SaveChanges();

                    var nodes = db.CompetitionGridNodes.Include(x => x.CompetitionGrid).Include(x => x.CompetitionGrid.CompetitionCategory).Include(x => x.Match).ThenInclude(x => x.Tatami)
                     .Where(x => x.CompetitionGrid.CompetitionCategory.CompetitionId == tatami.CompetitionId && x.Match != null)
                     .ToList();

                    var numbering = new NumberingOfMatches();
                    nodes = numbering.SetMatchesGridNumber(nodes);
                    nodes = numbering.SetMatchesGlobalNumber(nodes);
                    var matches = numbering.InitTatamiMatchNumber(db.CompetitionGridNodes.Include(x => x.CompetitionGrid).ToList(), db.Matches.ToList(), db.Tatamis.ToList());

                    db.Matches.UpdateRange(matches);
                    db.UpdateRange(nodes);
                    db.SaveChanges();

                    response.IsSuccess = true;

                }
                catch (Exception e)
                {
                    response.ExceptionMessage = e.Message;
                    response.IsSuccess = false;
                }
            }
            return response;
        }

        public object Any(ChangeTatamiForCategory request)
        {
            ExceptionResponse response = new ExceptionResponse();
            using (var db = dbContextFactory.CreateDbContext())
            {
                try
                {
                    var competitionGrid = db.CompetitionGrids.FirstOrDefault(x => x.CompetitionCategoryId == request.CategoryId);
                    var tatami = db.Tatamis.FirstOrDefault(x => x.Id == request.TatamiId);

                    if (competitionGrid == null) throw new ArgumentNullException("Competition grid doesn't exist");
                    if (tatami == null) throw new ArgumentNullException("Tatami doesn't exist");

                    var matches = db.Matches.Where(m => m.CompetitionGridId == competitionGrid.Id).ToList();
                    foreach (var match in matches)
                    {
                        match.Tatami = tatami;
                    }
                    db.Matches.UpdateRange(matches);
                    db.SaveChanges();

                    var nodes = db.CompetitionGridNodes.Include(x => x.CompetitionGrid).Include(x => x.CompetitionGrid.CompetitionCategory).Include(x => x.Match).ThenInclude(x => x.Tatami)
                    .Where(x => x.CompetitionGrid.CompetitionCategory.CompetitionId == tatami.CompetitionId && x.Match != null)
                    .ToList();

                    var numbering = new NumberingOfMatches();
                    nodes = numbering.SetMatchesGridNumber(nodes);
                    nodes = numbering.SetMatchesGlobalNumber(nodes);

                    var matchesWithNumber = numbering.InitTatamiMatchNumber(db.CompetitionGridNodes.Include(x => x.CompetitionGrid).ToList(), db.Matches.ToList(), db.Tatamis.ToList());
                    db.Matches.UpdateRange(matchesWithNumber);
                    db.UpdateRange(nodes);
                    db.SaveChanges();

                    response.IsSuccess = true;
                }
                catch (Exception e)
                {
                    response.ExceptionMessage = e.Message;
                    response.IsSuccess = false;
                }
            }
            return response;
        }

        public object Any(GetMatchesForTatami request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var matches = db.Matches.Where(m => m.TatamiId == request.TatamiId && m.MatchInfo!=MatchStatus.Finished).OrderBy(x=>x.TatamiMatchNumber).ToList();
                return matches.ConvertObjects<Match, UiMatch>();
            }
        }

        public object Any(GetMatchesForAllTatamies request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var matches = db.Matches.Where(m=>m.MatchInfo!=MatchStatus.Finished).OrderBy(x=>x.TatamiMatchNumber).ToList();
                return matches.ConvertObjects<Match, UiMatch>();
            }
        }
    }
}

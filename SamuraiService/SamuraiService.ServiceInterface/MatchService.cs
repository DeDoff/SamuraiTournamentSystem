using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.MatchService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class MatchService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public object Any(CreateMatch request)
        {
            if (request.Match == null)
            {
                return new Match();
            }
            var match = request.Match.ConvertObject<UiMatch, Match>();
            using (var db = dbContextFactory.CreateDbContext())
            {
                db.Matches.Add(match);
            }
            return match;

        }

        public object Any(UpdateMatch request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var match = db.Matches.SingleOrDefault(x => x.Id == request.Match.id);
                if (match != null)
                {
                    foreach (var outputObjectFiled in match.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.Match.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.Match) != outputObjectFiled.GetValue(match))
                                {
                                    outputObjectFiled.SetValue(match, inputObjectField.GetValue(request.Match));
                                }
                            }
                        }
                    }

                    db.Matches.Update(match);
                    db.SaveChanges();
                }
                return match.Id;
            }
        }

        public object Any(DeleteMatch request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var match = db.Matches.SingleOrDefault(m => m.Id == request.MatchId);
                    if (match != null)
                    {
                        db.Matches.Remove(match);
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

        public object Any(GetAllMatches request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var matches = db.Matches.ToList();
                return matches;
            }
        }

        public object Any(ActivateMatch request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                using (var db = dbContextFactory.CreateDbContext())
                {
                    var match = db.Matches.SingleOrDefault(x => x.Id == request.MatchId);
                    if (db.Matches.Where(m => m.TatamiId == match.TatamiId && m.MatchInfo == MatchStatus.Active).ToList().Count > 0) throw new ArgumentException("Активный матч уже есть");
                    if (match == null) throw new ArgumentNullException("Матча с таким идентификатором не существует");
                    if (match.MatchInfo == MatchStatus.Finished) throw new FormatException("Матч уже завершен");
                    match.MatchInfo = MatchStatus.Active;
                    db.Matches.Update(match);
                    db.SaveChanges();
                }
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ExceptionMessage = e.Message;
            }
            return response;
        }

        public object Any(FinishMatchAndLoadNext request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                using (var db = dbContextFactory.CreateDbContext())
                {
                    var match = db.Matches.SingleOrDefault(m => m.Id == request.FinishMatch.id);

                    if (match == null) throw new ArgumentNullException("Матча с таким ID не существует");
                    if (match.MatchInfo == MatchStatus.NotStarted) throw new ArgumentException("Матч еще не начат");

                    match.MatchInfo = MatchStatus.Finished;
                    match.MatchDuration = request.FinishMatch.matchDuration;
                    match.WinnerId = request.FinishMatch.winnerId;

                    db.Matches.Update(match);
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
}

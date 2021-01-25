using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiLogic.Tournaments;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceInterface.Logic;
using SamuraiService.ServiceModel.CompetitionService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamuraiService.ServiceInterface
{
    public class CompetitionService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public object Any(GetAllCompetitionInfo request)
        {
            using var db = dbContextFactory.CreateDbContext();
            var competition = db.Competitions
                .AsNoTracking()
                .Include(x => x.Categories)
                .ThenInclude(x => x.CompetitionGrid)
                .FirstOrDefault(x => x.Id == request.CompetitionId);

            return competition;
        }

        public object Any(GetCompetitionGrid request)
        {
            using var db = dbContextFactory.CreateDbContext();
            CompetitionGrid competitionGrid = null;

            var tournamentGridExpression = db.CompetitionGrids
                .AsNoTracking()
                .Include(x => x.Nodes);

            competitionGrid = request.CompetitionGridId == 0 ?
                tournamentGridExpression.FirstOrDefault(x => x.MatchesConunt == db.CompetitionGrids.Max(y => y.MatchesConunt))
                : tournamentGridExpression.FirstOrDefault(x => x.Id == request.CompetitionGridId);

            return competitionGrid == null ? null :
                new BracketHelper().CreateUiBracket(competitionGrid.Nodes.ToList());
        }

        public object Any(GetTestCompetitionGrid request)
        {
            if (request.CompetitorsCount < 2)
                return null;

            Random rnd = new Random();
            List<Competitor> competitors = new List<Competitor>();
            for (int i = 1; i <= request.CompetitorsCount; i++)
            {
                competitors.Add(new Competitor()
                {
                    Id = i,
                    Level = rnd.Next(0, 3),
                    FirstName = i.ToString() + " спортсмен",
                });
            }

            var nodes = new OlympicTournametSystem().CreateGrid(competitors, TournamentParameters.Default).Distinct().ToList();

            var n = nodes.Where(x => x.Match != null).Distinct().OrderBy(x => x.CompetitorRest).ToList();
            for (int i = 1; i <= n.Count(); i++)
            {
                n[i - 1].GridMatchNumber = i;
            }

            var bracket = new BracketHelper().CreateUiBracket(nodes);
            for (int i = 0; i < bracket.bracketNodes.Length; i++)
            {
                var node = bracket.bracketNodes[i];
                if (node.left != null && node.right != null)
                {
                    node.winner = rnd.Next(0, 2) == 1 ? node.left : node.right;
                    node.matchId = i + 1;
                }
            }
            return bracket;
        }

        public int Any(CreateCompetition request)
        {
            if (request.Competition == null)
                return -1;

            var competition = request.Competition.ConvertObject<UiCompetition, Competition>();

            using (var db = new SamuraiDbContext())
            {
                db.Competitions.Add(competition);
                db.SaveChanges();

                return competition.Id;
            }
        }

        public object Any(DeleteCompetition request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var competition = db.Competitions.SingleOrDefault(x => x.Id == request.CompetitionId);
                    if (competition != null)
                    {
                        db.Competitions.Remove(competition);
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

        public object Any(GetAllCompetitions request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var competitions = db.Competitions.AsNoTracking().ToList();
                return competitions.ConvertObjects<Competition, UiCompetition>().ToList();
            }
        }

        public int Any(UpdateCompetition request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var competition = db.Competitions.SingleOrDefault(x => x.Id == request.Competition.id);
                if (competition != null)
                {
                    foreach (var outputObjectFiled in competition.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.Competition.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.Competition) != outputObjectFiled.GetValue(competition))
                                {
                                    outputObjectFiled.SetValue(competition, inputObjectField.GetValue(request.Competition));
                                }
                            }
                        }
                    }

                    db.Competitions.Update(competition);
                    db.SaveChanges();
                }
                return competition.Id;
            }
        }

        public object Any(CancelMatchesForCompetition request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                CompetitionRollback crb = new CompetitionRollback();
                crb.RemoveMatches(request.CompetitionId);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ExceptionMessage = e.Message;
            }
            return response;
        }

        public object Any(RemoveCompetitorsFromCompetition request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                CompetitionRollback crb = new CompetitionRollback();
                crb.RemoveCompetitors(request.CompetitionId);
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ExceptionMessage = e.Message;
            }
            return response;
        }

        public object Any(CreateCompetitionGrid request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                using (var db = dbContextFactory.CreateDbContext())
                {
                    var category = db.CompetitionCategories.Include(x=>x.Competitors).SingleOrDefault(x => x.Id == request.CompetitionCategoryId);
                    int k = 1;
                    if (category.Competitors.Count < 2) throw new ArgumentException("Недостаточное число участников в категории");

                    var gridNodes = new MatchCreator(new OlympicTournametSystem()).CreateGrid(category.Competitors, TournamentParameters.Default);
                    var matches = gridNodes.Select(x => x.Match).Where(x => x != null).ToList();
                    matches.ForEach(m => m.Tatami = db.Tatamis.FirstOrDefault());
                    category.CompetitionGrid = new CompetitionGrid() { Matches = matches.ToList(), MatchesConunt = (short)matches.Count, IsFinished = false, Nodes = gridNodes, TatamiOrder = k++ };
                    db.CompetitionCategories.Update(category);
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

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SamuraiAPI.resource;
using SamuraiDbModel;
using SamuraiLogic.CompetitionHistory;
using SamuraiService.ServiceInterface;
using SamuraiService.ServiceModel.MatchService;
using SamuraiService.ServiceModel.TatamiService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ApiManager apiManager = new ApiManager();
            //apiManager.Manage();

            DataCreataor data = new DataCreataor();
            Initialization(data);

            Console.WriteLine("Hello World!"); 
        }

        private static void Initialization(IDataCreator dataCreator)
        {
            dataCreator.InitDbDefault();

            var competitionId = dataCreator.CreateCompetition(3);
            dataCreator.ShuffleMatchesBetweenTatamis(competitionId);
            dataCreator.MatchNumbering(competitionId);


            using (var db = new SamuraiDbContext())
            {
                var competition = db.Competitions
                    .Include(x => x.Categories)
                    .ThenInclude(x => x.CompetitionGrid)
                    .ThenInclude(x => x.Matches)
                    .ThenInclude(x => x.Competitors)
                    .ThenInclude(x => x.AgeGroup)

                    .Include(x => x.Categories)
                    .ThenInclude(x => x.CompetitionGrid)
                    .ThenInclude(x => x.Matches)
                    .ThenInclude(x => x.Competitors)
                    .ThenInclude(x => x.WeightGroup)

                    .Include(x => x.Categories)
                    .ThenInclude(x => x.CompetitionGrid)
                    .ThenInclude(x => x.Matches)
                    .ThenInclude(x => x.Competitors)
                    .ThenInclude(x => x.Sex)

                    .Include(x => x.Categories)
                    .ThenInclude(x => x.CompetitionGrid)
                    .ThenInclude(x => x.Matches)
                    .ThenInclude(x => x.Competitors)
                    .ThenInclude(x => x.Grade)

                    .FirstOrDefault();

                var history = CompetitionHistoryBuilder.GetHistory(competition);

            }
        }
    }
}

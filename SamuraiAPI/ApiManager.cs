using SamuraiDbModel;
using SamuraiService.ServiceInterface;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.CompetitionService;
using SamuraiService.ServiceModel.SportCategoriesService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI
{
    public class ApiManager
    {
        private DataCreatorApi dataCreator = new DataCreatorApi();
        public void Manage()
        {
            try
            {
                
                UiCompetition competition = dataCreator.CreateCompetition();

                List<UiTatami> tatamis = dataCreator.CreateTatamis(competition.id);

                List<UiCompetitionCategory> competitionCategories = dataCreator.CreateCompetitionCategories(competition.id);

                List<UiReferee> referees = dataCreator.CreateRefereesFromTainers();

                List<UiCompetitor> uiCompetitors = dataCreator.CreateCompetitors(competition.id);
                
                dataCreator.CreateGrids(competitionCategories.Select(x=>x.id).ToList());
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

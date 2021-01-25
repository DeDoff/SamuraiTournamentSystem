using Microsoft.Extensions.Configuration;
using SamuraiDbModel;
using SamuraiService.ServiceInterface;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.CompetitionCategoryService;
using SamuraiService.ServiceModel.CompetitionService;
using SamuraiService.ServiceModel.CompetitorService;
using SamuraiService.ServiceModel.SportCategoriesService;
using SamuraiService.ServiceModel.SportsmanService;
using SamuraiService.ServiceModel.TatamiService;
using SamuraiService.ServiceModel.TrainerService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using ServiceStack.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI
{
    public class DataCreatorApi
    {
        public class AppHost : AppSelfHostBase
        {

            public AppHost() : base("HttpListener Self-Host", typeof(TatamiService).Assembly) 
            {

            }

            public override void Configure(Funq.Container container)
            {
                Licensing.RegisterLicense(@"8512-e1JlZjo4NTEyLE5hbWU6TGlua2VyIFNvbHV0aW9ucy
                xUeXBlOkluZGllLE1ldGE6MCxIYXNoOmd6alNmYzNzM1d6T
                UZMVDR5SVlrUC9JeXVjVHFHTTl3clR6Z2FOTmhtZHdiREk5
                WjBVejB0dHVvVmtqLysvNGdnVXU1Zm4rd2NRUFcxOHV0ZDl
                Hakw3ckxrL29zTGQwTGJpV3ZRcm1zUkpsSmREOWFTY292MF
                FSMXAwbktJWm5leE9vSE0rbnJ1V1BPc3oybXdLaS9SbkUwd
                UE4ZEt5NHVUSVdqRVhxamtwVT0sRXhwaXJ5OjIwMjEtMDkt
                MTR9");

                
            }
        }

        JsonServiceClient client;

        public DataCreatorApi()
        {
            var defaultUrl = "http://localhost:5000/";
            var configFileName = "appsettings.json";
            var appSettingsConnetionStringKey = "ConnectionUrl";

            IConfiguration configuration = new ConfigurationBuilder()
              .AddJsonFile(configFileName, true, true)
              .Build();

            var connectionString = configuration.GetSection(appSettingsConnetionStringKey)?.Value;
            if(connectionString.IsNullOrEmpty()) client = new JsonServiceClient(defaultUrl);
            else client = new JsonServiceClient(connectionString);
        }

        public UiCompetition CreateCompetition()
        {
            Console.WriteLine("Полное навзание соревнования: ");
            string name = Console.ReadLine();
            Console.WriteLine("Краткое навзание соревнования: ");
            string shortName = Console.ReadLine();
            UiCompetition competition = new UiCompetition()
            {
                name = name,
                shortName = shortName,
                startDate = DateTime.Now.AddDays(1),
                endDate = DateTime.Now.AddDays(2)
            };

            competition.id = client.Post(new CreateCompetition() { Competition = competition });

            return competition;
        }

        public List<UiCompetitionCategory> CreateCompetitionCategories(int competitionId)
        {
            List<UiSportCategory> uiSportCategories = client.Get(new GetAllSportCategories()).ToList();
            List<UiCompetitionCategory> uiCompetitionCategories = new List<UiCompetitionCategory>();


            foreach (var sportCategory in uiSportCategories)
            {

                uiCompetitionCategories.Add(new UiCompetitionCategory()
                {
                    competitionId = competitionId,
                    name = sportCategory.name,
                    weightFrom = sportCategory.weightGroup.from,
                    weightTo = sportCategory.weightGroup.to,
                    ageFrom = sportCategory.ageGroup.from,
                    ageTo = sportCategory.ageGroup.to,
                    sportCategoryId = sportCategory.id,
                    tatamiId = 1
                });
            }

            foreach (var competCat in uiCompetitionCategories)
            {
                Console.WriteLine(uiCompetitionCategories.IndexOf(competCat));
                client.Post(new CreateCompetitionCategory() { CompetitionCategory = competCat });
            }

            return uiCompetitionCategories;
        }

        public List<UiTatami> CreateTatamis(int CompetitionId)
        {
            Console.WriteLine("Введите число татами: ");
            int tatamiCount = int.Parse(Console.ReadLine());
            var tatamis = new List<UiTatami>();

            for (int i = 1; i <= tatamiCount; i++)
            {
                tatamis.Add(new UiTatami() { competitionId = CompetitionId, name = $"Татами {i}", matchPrefix = $"T{i}-" });
            }


            foreach (var tatami in tatamis)
            {
                client.Post(new CreateTatami() { Tatami = tatami });
            }
            return tatamis;
        }
        public List<UiReferee> CreateRefereesFromTainers()
        {
            List<UiReferee> referees = new List<UiReferee>();

            var trainers = client.Get(new GetAllTrainers());

            Random rnd = new Random();
            var l = Enum.GetNames(typeof(ReferreeType)).Length;
            foreach (var trainer in trainers)
            {
                referees.Add(new UiReferee()
                {
                    age = trainer.age,
                    firstName = trainer.firstName,
                    lastName = trainer.lastName,
                    birthDate = trainer.birthDate,
                    weight = trainer.weight,
                    grade = trainer.grade,
                    sex = trainer.sex,
                    referreeType = (ReferreeType)rnd.Next(0, l - 1)
                });
            }

            return referees;

        }

        public List<UiCompetitor> CreateCompetitors(int id)
        {
            List<UiCompetitor> competitors = new List<UiCompetitor>();
            var sportsmens = client.Get(new GetAllSportsmans());
            var ageGroups = client.Get(new GetAllAgeGroups());
            var weightGroups = client.Get(new GetAllWeightGroups());
            var sportCategories = client.Get(new GetAllSportCategories());

            sportsmens.ToList().ForEach(x =>
             {
                 var competitor = new UiCompetitor()
                 {
                     competitionId = id,
                     age = x.age,
                     firstName = x.firstName,
                     lastName = x.lastName,
                     weight = x.weight,
                     sportsmanId = x.id,
                     iko = x.iko,
                     birthDate = x.birthDate,
                     sex = x.sex,
                     grade = x.grade,
                 };

                 competitors.Add(competitor);
             });

            foreach (var compet in competitors)
            {
                client.Post(new CreateCompetitor() { Competitor = compet });
            }

            return competitors;
        }

        public void CreateGrids(List<int> uiCategoryIds)
        {
            foreach (var uiCat in uiCategoryIds)
            {
                client.Post(new CreateCompetitionGrid() { CompetitionCategoryId = uiCat });
            }
        }
    }
}

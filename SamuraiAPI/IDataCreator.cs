using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI
{
    public interface IDataCreator
    {
        void InitDbDefault();

        List<SportClub> CreateDefaultClubs();
        List<Trainer> CreateDefaultTrainers(List<Grade> grades, List<Sex> sexes, List<AgeGroup> ages, List<WeightGroup> weights);
        List<SportCategoryType> CreateDefaultTypes();
        List<WeightGroup> CreateDefaultWeightGroups();
        List<Grade> CreateDefaultGrades();
        List<Sex> CreateDefaultSexes();
        List<AgeGroup> CreateDefaultAgeGroups();
        List<SportCategory> CreateSportCategories(List<AgeGroup> ageGroups, List<WeightGroup> weightGroups, List<SportCategoryType> types, List<Sex> sexes);


        List<Sportsman> GenerateSportsmans(int sportnsmansCount, List<Sex> sexes, List<Grade> grades, List<AgeGroup> ages, List<WeightGroup> weights);
       
        int CreateCompetition(int tatamiCount = 1);
        void ShuffleMatchesBetweenTatamis(int competitionId = 0);
        public void MatchNumbering(int competitionId);

    }
}

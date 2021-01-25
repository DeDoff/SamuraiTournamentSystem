using SamuraiDbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface.Logic
{
    public static class FillDBCompetitorsFromCSV
    {
        public static bool FillDB(List<Competitor> competitors)
        {
            using (var db = new SamuraiDbContext())
            {
                foreach (var competitor in competitors)
                {
                    int competId = competitor.CompetitionId;
                    competitor.Competition = db.Competitions.FirstOrDefault(x => x.Id == competId);
                    //competitor.AgeGroup = db.AgeGroups.FirstOrDefault(a => (competitor.Age >= a.From && competitor.Age <= a.To && a.To != 0) || (competitor.Age >= a.From && a.To == 0));
                    //competitor.WeightGroup = db.WeightGroups.FirstOrDefault(a => (competitor.Weight >= a.From && competitor.Weight <= a.To && a.To != 0) || (competitor.Weight >= a.From && a.To == 0));

                    if (competitor.FirstName == string.Empty || competitor.LastName == string.Empty) throw new IndexOutOfRangeException("Incorrect number of required columns");

                    if (db.Competitors.SingleOrDefault(c => c.FirstName == competitor.FirstName && c.LastName == competitor.LastName) != null) continue;
                    else
                    {
                        var sportsmen = db.Sportsmens.SingleOrDefault(x => x.FirstName == competitor.FirstName && x.LastName == competitor.LastName);
                        if (sportsmen != null)
                        {
                            sportsmen.Weight = competitor.Weight;
                            sportsmen.Age = competitor.Age;
                            sportsmen.Grade = competitor.Grade;

                            db.Sportsmens.Update(sportsmen);
                            competitor.SportsmanId = sportsmen.Id;
                            db.Competitors.Add(competitor);
                            db.SaveChanges();
                        }
                        else
                        {
                            var sportClub = db.SportClubs.SingleOrDefault(x => x.Name == "Клуб Глобал");
                            var trainer = db.Trainers.FirstOrDefault(x => x.FirstName == "Вадим" && x.LastName == "Усов");

                            Sportsman sportsman = CreateSportsmanFromCompetitor(competitor, sportClub, trainer);
                            db.Sportsmens.Add(sportsman);
                            db.SaveChanges();

                            competitor.SportsmanId = sportsman.Id;
                            db.Competitors.Add(competitor);
                            db.SaveChanges();

                        }
                    }

                }
            }
            return true;
        }

        private static Sportsman CreateSportsmanFromCompetitor(Competitor competitor, SportClub sportClub, Trainer trainer)
        {
            Sportsman sportsman = new Sportsman()
            {
                FirstName = competitor.FirstName,
                LastName = competitor.LastName,
                SportClub = sportClub,
                Age = competitor.Age,
                Weight = competitor.Weight,
                Grade = competitor.Grade,
                IKO = competitor.IKO,
                Sex = competitor.Sex,
                Trainer = trainer
            };
            return sportsman;
        }
    }
}

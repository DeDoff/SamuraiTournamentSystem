using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface.Logic
{
    public class CompetitionRollback
    {
        public void RemoveMatches(int id)
        {
            using (var db = new SamuraiDbModel.SamuraiDbContext())
            {
                if (db.Competitions.SingleOrDefault(x => x.Id == id) == null) throw new ArgumentNullException($"Сорвенования с ID = {id} не существует");
                var competitionCategories = db.CompetitionCategories.Where(x => x.CompetitionId == id ).Select(x => x.Id).ToList();
                var competitionGridIds = db.CompetitionGrids.Where(x => competitionCategories.Contains(x.CompetitionCategoryId)).Select(x => x.Id).ToList();
                var matches = db.Matches.Where(x => competitionGridIds.Contains(x.CompetitionGridId)).ToList();
                var grids = db.CompetitionGridNodes.Where(x => competitionGridIds.Contains(x.CompetitionGridId)).ToList();
                db.RemoveRange(grids);
                db.RemoveRange(matches);
                db.SaveChanges();
            }
        }

        public void RemoveCompetitors(int id)
        {
            RemoveMatches(id);
            using (var db = new SamuraiDbModel.SamuraiDbContext())
            {
                if (db.Competitions.SingleOrDefault(x => x.Id == id) == null) throw new ArgumentNullException($"Сорвенования с ID = {id} не существует");
                var competitors = db.Competitors.Where(x => x.CompetitionId == id).ToList();
                db.RemoveRange(competitors);
                db.SaveChanges();
            }
        }
    }
}

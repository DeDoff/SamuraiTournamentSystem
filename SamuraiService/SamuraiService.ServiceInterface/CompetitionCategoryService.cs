using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.CompetitionCategoryService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class CompetitionCategoryService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateCompetitionCategory request)
        {
            if (request.CompetitionCategory == null)
                return -1;
            var competitionCategory = request.CompetitionCategory.ConvertObject<UiCompetitionCategory, CompetitionCategory>();

            using (var db = dbContextFactory.CreateDbContext())
            {
                if(competitionCategory.Competitors==null)
                competitionCategory.Competitors = new List<Competitor>();
                competitionCategory.Tatami = db.Tatamis.FirstOrDefault();
                db.CompetitionCategories.Add(competitionCategory);
                db.SaveChanges();
            }
            return competitionCategory.Id;
        }

        public object Any(DeleteCompetitionCategory request)
        {
            ExceptionResponse response = new ExceptionResponse();
            try
            {
                using (var db = dbContextFactory.CreateDbContext())
                {
                    var competitionCategory = db.CompetitionCategories.SingleOrDefault(x => x.Id == request.CompetitionCategoryId);
                    if (competitionCategory == null) throw new ArgumentNullException("Сорвеновательной категории с таким ID не существует!");
                    db.CompetitionCategories.Remove(competitionCategory);
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

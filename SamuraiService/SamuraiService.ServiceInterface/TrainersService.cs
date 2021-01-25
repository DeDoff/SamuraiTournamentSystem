using Microsoft.EntityFrameworkCore;
using SamuraiDbModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.TrainerService;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiService.ServiceInterface
{
    public class TrainersService:Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public object Any(GetAllTrainers request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var trainers = db.Trainers.ToList();
                return trainers.ConvertObjects<Trainer, UiTrainer>();
            }
        }

    }
}

using ServiceStack;
using SamuraiService.ServiceModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SamuraiDbModel;
using SamuraiService.ServiceModel.SportCategoryServiceModel;
using SamuraiService.ServiceInterface.Extensions;
using SamuraiService.ServiceModel.UiDataModel;
using System.Collections.Generic;
using SamuraiService.ServiceModel.SportCategoriesService;
using System;

namespace SamuraiService.ServiceInterface
{
    public class SportCategoriesService : Service
    {
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        public int Any(CreateSportCategory request)
        {
            if (request.SportCategory == null)
                return -1;

            var sportCategory = request.SportCategory.ConvertObject<UiSportCategory, SportCategory>();
            sportCategory.WeightGroup = request.SportCategory.weightGroup.ConvertObject<UiWeightGroup, WeightGroup>();
            sportCategory.AgeGroup = request.SportCategory.ageGroup.ConvertObject<UiAgeGroup, AgeGroup>();


            using (var db = dbContextFactory.CreateDbContext())
            {
                sportCategory.AgeGroup = db.AgeGroups.Where(x => x.Id == request.SportCategory.ageGroup.id).FirstOrDefault();
                sportCategory.WeightGroup = db.WeightGroups.Where(x => x.Id == request.SportCategory.weightGroup.id).FirstOrDefault();
                sportCategory.Type = db.Types.Where(x => x.Name.ToLower() == request.SportCategory.type.ToLower()).FirstOrDefault();

                db.SportCategories.Add(sportCategory);
                db.SaveChanges();
                return sportCategory.Id;
            }
        }

        public object Any(GetAllSportCategories request)
        {
            List<SportCategory> sportCategories;
            using (var db = dbContextFactory.CreateDbContext())
            {
                sportCategories = db.SportCategories.Include(x => x.WeightGroup).Include(x => x.AgeGroup).Include(x=>x.Sex).Include(x=>x.Type).AsNoTracking().ToList();
            }
            
            return sportCategories.ToUiModel();
        }

        public object Any(DeleteSportCategory request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var sportCategory = db.SportCategories.SingleOrDefault(x => x.Id == request.SportCategoryId);
                    if (sportCategory == null) throw new ArgumentNullException($"Категории с ID = {request.SportCategoryId} не существует");

                    db.SportCategories.Remove(sportCategory);
                    db.SaveChanges();
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

        public object Any(UpdateSportCategory request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var category = db.SportCategories.Include(x => x.WeightGroup).Include(x => x.AgeGroup).SingleOrDefault(x => x.Id == request.SportCategory.id);
                if (category != null)
                {
                    foreach (var outputObjectFiled in category.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.SportCategory.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.SportCategory) != outputObjectFiled.GetValue(category))
                                {
                                    outputObjectFiled.SetValue(category, inputObjectField.GetValue(request.SportCategory));
                                }
                            }
                        }
                    }

                    db.SportCategories.Update(category);
                    db.SaveChanges();
                }
                return category.Id;
            }
        }

        public int Any(CreateAgeGroup request)
        {
            if (request.AgeGroup == null)
                return -1;

            var ageGroup = request.AgeGroup.ConvertObject<UiAgeGroup, AgeGroup>();
            using (var db = dbContextFactory.CreateDbContext())
            {

                db.AgeGroups.Add(ageGroup);
                db.SaveChanges();
                return ageGroup.Id;
            }
        }

        public object Any(GetAllAgeGroups request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                return db.AgeGroups.AsNoTracking().ToList().ConvertObjects<AgeGroup, UiAgeGroup>();
            }
        }

        public object Any(DeleteAgeGroup request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var ageGroup = db.AgeGroups.SingleOrDefault(x => x.Id == request.AgeGroupId);
                    if (ageGroup == null) throw new ArgumentNullException($"Возрастной группы с ID = {request.AgeGroupId} не существует");

                    db.AgeGroups.Remove(ageGroup);
                    db.SaveChanges();
                    response.IsSuccess = true;

                }
                catch (Exception e)
                {
                    response.ExceptionMessage = e.Message;
                    response.IsSuccess = false;
                }
                return response;
            }
        }

        public object Any(UpdateAgeGroup request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var ageGroup = db.AgeGroups.SingleOrDefault(x => x.Id == request.AgeGroup.id);
                if (ageGroup != null)
                {
                    foreach (var outputObjectFiled in ageGroup.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.AgeGroup.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.AgeGroup) != outputObjectFiled.GetValue(ageGroup))
                                {
                                    outputObjectFiled.SetValue(ageGroup, inputObjectField.GetValue(request.AgeGroup));
                                }
                            }
                        }
                    }

                    db.AgeGroups.Update(ageGroup);
                    db.SaveChanges();

                }
                return ageGroup.Id;
            }
        }

        public object Any(CreateWeightGroup request)
        {
            if (request.WeightGroup == null)
                return -1;

            var weightGroup = request.WeightGroup.ConvertObject<UiWeightGroup, WeightGroup>();

            weightGroup.Name = "От " + weightGroup.From + " До " + weightGroup.To;

            using (var db = dbContextFactory.CreateDbContext())
            {
                db.WeightGroups.Add(weightGroup);
                db.SaveChanges();
                return weightGroup;
            }
        }

        public object Any(GetAllWeightGroups request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                return db.WeightGroups.AsNoTracking().ToList().ConvertObjects<WeightGroup, UiWeightGroup>();
            }
        }

        public object Any(DeleteWeightGroup request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                ExceptionResponse response = new ExceptionResponse();
                try
                {
                    var weightGroup = db.WeightGroups.SingleOrDefault(x => x.Id == request.WeightGroupId);
                    if (weightGroup == null) throw new ArgumentNullException($"Весовой группы с ID = {request.WeightGroupId} не существует");

                    db.WeightGroups.Remove(weightGroup);
                    db.SaveChanges();
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

        public object Any(UpdateWeightGroup request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var weightGroup = db.WeightGroups.SingleOrDefault(x => x.Id == request.WeightGroup.id);
                if (weightGroup != null)
                {
                    foreach (var outputObjectFiled in weightGroup.GetType().GetProperties())
                    {
                        foreach (var inputObjectField in request.WeightGroup.GetType().GetProperties())
                        {
                            if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                            {
                                if (inputObjectField.GetValue(request.WeightGroup) != outputObjectFiled.GetValue(weightGroup))
                                {
                                    outputObjectFiled.SetValue(weightGroup, inputObjectField.GetValue(request.WeightGroup));
                                }
                            }
                        }
                    }

                    db.WeightGroups.Update(weightGroup);
                    db.SaveChanges();

                }
                return weightGroup.Id;
            }
        }


    }
}

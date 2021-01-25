using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiService.ServiceInterface.Extensions
{
    public static class UserModelExtensions
    {
        public static ServiceModel.UiDataModel.UiUser ToUiModel(this SamuraiDbModel.User user)
        {
            var uiUser = CoppyFields<ServiceModel.UiDataModel.UiUser>(user);
            uiUser.selectedRoles = user.Roles.ConvertAll(x => CoppyFields<ServiceModel.UiDataModel.UiRole>(x)).ToArray();
            return uiUser;
        }

        public static IEnumerable<ServiceModel.UiDataModel.UiUser> ToUiModel(this IEnumerable<SamuraiDbModel.User> users)
        {
            foreach (var user in users)
            {
                yield return user.ToUiModel();
            }
        }

        public static IEnumerable<ServiceModel.UiDataModel.UiSportCategory> ToUiModel(this IEnumerable<SamuraiDbModel.SportCategory> sportCategories)
        {
            foreach (var category in sportCategories)
            {
                yield return category.ToUiModel();
            }
        }
        public static ServiceModel.UiDataModel.UiSportCategory ToUiModel(this SamuraiDbModel.SportCategory sportCategory)
        {
            var uiSportCategory = CoppyFields<ServiceModel.UiDataModel.UiSportCategory>(sportCategory);
            uiSportCategory.ageGroup = CoppyFields<ServiceModel.UiDataModel.UiAgeGroup>(sportCategory.AgeGroup);
            uiSportCategory.weightGroup = CoppyFields<ServiceModel.UiDataModel.UiWeightGroup>(sportCategory.WeightGroup);
            uiSportCategory.weightGroup.sex = sportCategory.Sex.Name;
            uiSportCategory.type = sportCategory.Type.Name;
            return uiSportCategory;
        }


        public static TT ConvertObject<T, TT>(this T inputObject)
        {
            return CoppyFields<TT>(inputObject);
        }

        public static IEnumerable<TT> ConvertObjects<T, TT>(this IEnumerable<T> inputObjects)
        {
            foreach (var inputObject in inputObjects)
            {
                yield return CoppyFields<TT>(inputObject);
            }
        }

        private static T CoppyFields<T>(object o)
        {
            var outputObject = Activator.CreateInstance<T>();
            if (o == null)
            {
                return outputObject;
            }

            var outputObjectFileds = outputObject.GetType().GetAllProperties();
            var inputObjectFields = o.GetType().GetAllProperties();
            foreach (var outputObjectFiled in outputObjectFileds)
            {
                foreach (var inputObjectField in inputObjectFields)
                {
                    if (outputObjectFiled.Name.ToLower() == inputObjectField.Name.ToLower() && outputObjectFiled.PropertyType == inputObjectField.PropertyType)
                    {
                        outputObjectFiled.SetValue(outputObject, inputObjectField.GetValue(o));
                    }
                }
            }

            return outputObject;
        }
    }
}

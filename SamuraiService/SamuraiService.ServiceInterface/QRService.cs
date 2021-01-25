using SamuraiService.ServiceModel.QRService;
using SamuraiService.ServiceInterface.Extensions;
using ServiceStack;
using System;
using System.Linq;
using SamuraiDbModel;
using Microsoft.EntityFrameworkCore;
using SamuraiService.ServiceModel.UiDataModel;
using ServiceStack.Configuration;
using QRCoder;

namespace SamuraiService.ServiceInterface
{
    public class QRService : Service
    {
        public IAppSettings AppSettings { get; set; }
        private static IDbContextFactory<SamuraiDbContext> dbContextFactory { get; set; } = HostContext.AppHost.Resolve<IDbContextFactory<SamuraiDbContext>>();

        private string GenarateUniqueUrl(int SportsmanId, int CompetitionId)
        {

            var defaultServerName = "defServer";
            var defaultMethodName = "defMethod";
            
            var serverName = AppSettings.Get<string>("ServerName");
            var methodName = AppSettings.Get<string>("MethodName");

            var method = string.IsNullOrEmpty(methodName) ? defaultMethodName : methodName;
            var server = string.IsNullOrEmpty(serverName) ? defaultServerName : serverName;

            string url = server + "/" + method + "/" + StringExtention.ToMD5(SportsmanId + "," + CompetitionId + "," + DateTime.Now);
            return url;
        }

        private byte[] GenerateBytes(string url)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
            
            return qrCodeAsBitmapByteArr;
        }

        public object Any(GenerateQR request)
        {
            string url = GenarateUniqueUrl(request.SportsmanId, request.CompetitionId);
            using (var db = dbContextFactory.CreateDbContext())
            {
                var existCompetitior = db.Competitors.Where(c => c.SportsmanId == request.SportsmanId && c.CompetitionId == request.CompetitionId).FirstOrDefault();
                if (existCompetitior != null)
                {
                    if (existCompetitior.RegistrationUrl == null)
                    {
                        existCompetitior.RegistrationUrl = url;

                        db.Competitors.Update(existCompetitior);
                        db.SaveChanges();
                    }
                    var imgBytes1 = GenerateBytes(existCompetitior.RegistrationUrl);
                    //ImgManager im1 = new ImgManager();
                    //im1.OpenImg(imgBytes1);
                    return imgBytes1;
                }

                Competitor competitor = new Competitor();
                var sportsmen = db.Sportsmens
                                    .Include(s => s.Grade)
                                    .Include(s => s.Sex)
                                    .Where(s => s.Id == request.SportsmanId)
                                    .FirstOrDefault();

                if (sportsmen == null) return -1;

                competitor = sportsmen.ConvertObject<Sportsman, Competitor>();
                competitor.CompetitionId = request.CompetitionId;
                
                competitor.SportsmanId = sportsmen.Id;
                competitor.RegistrationUrl = url;
                var competitorSportCategory = db.SportCategories
                             .FirstOrDefault(y => y.AgeGroup.Id == competitor.AgeGroup.Id
                                                 && y.Sex.Id == competitor.Sex.Id
                                                 && y.WeightGroup.Id == competitor.WeightGroup.Id);

                var competitionCategory = db.CompetitionCategories.Where(y => y.SportCategoryId == competitorSportCategory.Id).FirstOrDefault();
                competitor.CompetitionCategory = competitionCategory;

                db.Competitors.Add(competitor);
                db.SaveChanges();
                var imgBytes = GenerateBytes(url);
                //ImgManager im = new ImgManager();
                //im.OpenImg(imgBytes);
                return imgBytes;
            }
        }

        public object Any(GetInfoByUrl request)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var competitor = db.Competitors
                                    .Include(c => c.AgeGroup)
                                    .Include(c => c.WeightGroup)
                                    .Include(c => c.Sex)
                                    .Include(c => c.Grade)
                                    .Where(c => c.RegistrationUrl.ToLower()==request.Url.ToLower())
                                    .FirstOrDefault();
                var c = competitor.ConvertObject<Competitor, UiCompetitor>();
                c.ageGroup = competitor.AgeGroup.ConvertObject<AgeGroup, UiAgeGroup>();
                c.weightGroup = competitor.WeightGroup.ConvertObject<WeightGroup, UiWeightGroup>();
                c.grade = competitor.Grade.Name;
                c.weight = (int)competitor.Weight;
                c.weightGroup.sex = competitor.Sex.Name;

                return c;
            }
        }
    }
}

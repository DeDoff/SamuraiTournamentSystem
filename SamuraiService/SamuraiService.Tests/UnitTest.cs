using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using SamuraiService.ServiceInterface;
using SamuraiService.ServiceModel;
using SamuraiService.ServiceModel.SportCategoriesService;

namespace SamuraiService.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<UserServices>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
            //var service = appHost.Container.Resolve<UserServices>();
            var client = new JsonServiceClient("http://localhost:5000/");
            var response = client.Post(new CreateAgeGroup { AgeGroup = new ServiceModel.UiDataModel.UiAgeGroup(){id=1,ageName="pozhiloy",from=50,to=100 } });
            //var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.ToString(), Is.EqualTo(9));
        }
    }
}

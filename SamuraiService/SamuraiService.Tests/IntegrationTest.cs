using Funq;
using ServiceStack;
using NUnit.Framework;
using SamuraiService.ServiceInterface;
using SamuraiService.ServiceModel;
using SamuraiService.ServiceModel.SportCategoriesService;
using ServiceStack.Text;
using SamuraiService.ServiceModel.UiDataModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace SamuraiService.Tests
{
    public class IntegrationTest
    {
        const string BaseUri = "http://localhost:2000/";
        private readonly ServiceStackHost appHost;

        class AppHost : AppSelfHostBase
        {
            public AppHost() : base(nameof(IntegrationTest), typeof(UserServices).Assembly) { }

            public override void Configure(Container container)
            {
                JsConfig<System.DateTime>.SerializeFn = d => d.ToString("dd.mm.yyyy:HH.mm.ss");
                JsConfig.Init(new ServiceStack.Text.Config
                {
                    MaxDepth = 2,
                    IncludeNullValues = true,
                    IncludeNullValuesInDictionaries = true
                });

                Plugins.Add(new CorsFeature());
            }

            public new void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContextFactory<SamuraiDbModel.SamuraiDbContext>();
            }
        }

        public IntegrationTest()
        {
            appHost = new AppHost()
                .Init();
            var s = Microsoft.Extensions.Primitives.StringSegmentComparer.Ordinal;
            appHost.Start(BaseUri);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        public IServiceClient CreateClient() => new JsonServiceClient(BaseUri);

        [Test]
        public void Can_call_Hello_Service()
        {

            var client = CreateClient();
            
            //var response = client.Post(new CreateAgeGroup { AgeGroup = ag});

            //var response = client.Get(new Hello { Name = "World" });

            //Assert.That(response.ToString(), Is.EqualTo(ag.id));
        }
    }
}
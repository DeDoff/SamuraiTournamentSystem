using Funq;
using SamuraiService.ServiceInterface;
using ServiceStack;
using ServiceStack.Api.OpenApi;
using ServiceStack.Text;

namespace SamuraiService
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("SamuraiService", typeof(UserServices).Assembly) { }

        public override void Configure(Container container)
        {
            #region config
            SetConfig(new HostConfig
            {
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), false)
            });
            #endregion

            #region Json serializer
            JsConfig<System.DateTime>.SerializeFn = d => d.ToString("dd.mm.yyyy:HH.mm.ss");
            JsConfig.Init(new ServiceStack.Text.Config
            {
                MaxDepth = 2,
                IncludeNullValues = true,
                IncludeNullValuesInDictionaries = true
            });
            #endregion
            Plugins.Add(new OpenApiFeature());
            Plugins.Add(new CorsFeature());
        }
    }
}
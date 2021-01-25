using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Configuration;

namespace SamuraiService
{
    public class Startup : ModularStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextFactory<SamuraiDbModel.SamuraiDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Licensing.RegisterLicense(@"8512-e1JlZjo4NTEyLE5hbWU6TGlua2VyIFNvbHV0aW9ucy
                xUeXBlOkluZGllLE1ldGE6MCxIYXNoOmd6alNmYzNzM1d6T
                UZMVDR5SVlrUC9JeXVjVHFHTTl3clR6Z2FOTmhtZHdiREk5
                WjBVejB0dHVvVmtqLysvNGdnVXU1Zm4rd2NRUFcxOHV0ZDl
                Hakw3ckxrL29zTGQwTGJpV3ZRcm1zUkpsSmREOWFTY292MF
                FSMXAwbktJWm5leE9vSE0rbnJ1V1BPc3oybXdLaS9SbkUwd
                UE4ZEt5NHVUSVdqRVhxamtwVT0sRXhwaXJ5OjIwMjEtMDkt
                MTR9");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new MultiAppSettings(
                    new NetCoreAppSettings(Configuration)),
            });

        }
    }
}

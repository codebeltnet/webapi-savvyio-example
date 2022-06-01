using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Savvyio.Extensions;
using Savvyio.Extensions.Dapper;
using Savvyio.Extensions.DependencyInjection;
using Savvyio.Extensions.DependencyInjection.Dapper;
using Savvyio.Extensions.DependencyInjection.EFCore.Domain;
using Savvyio.Extensions.DependencyInjection.EFCore.Domain.EventSourcing;
using Savvyio.Extensions.EFCore.Domain.EventSourcing;
using Savvyio_Example.App;
using Savvyio_Example.App.Data;
using Savvyio_Example.App.Domain;


namespace Savvyio_Example
{
    public class Startup
    {
        private static bool FirstRun = false;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Savvyio_Example", Version = "v1" });
            });

            services.AddEfCoreAggregateDataSource(o =>
            {
                o.ContextConfigurator = b => b.UseInMemoryDatabase(nameof(LocationForecast)).EnableDetailedErrors().LogTo(Console.WriteLine);
                o.ModelConstructor = mb => mb.AddEventSourcing<LocationForecast, Guid>();
            }).AddEfCoreTracedAggregateRepository<LocationForecast, Guid>();

            services.AddDapperDataSource(o => o.ConnectionFactory = () =>
            {
                    var cnn = new SqliteConnection("Data Source=database.db");
                    if (!FirstRun)
                    {
                        cnn.Open();
                        cnn.Execute("DROP TABLE IF EXISTS WF");
                        cnn.Execute("CREATE TABLE WF (Date TEXT, TemperatureC INTEGER, Summary VARCHAR(32))");
                        FirstRun = true;
                    }
                    return cnn;

            }).AddDapperDataStore<WeatherForecastDataStore, WeatherForecast>();

            services.AddSavvyIO(o => o.EnableAutomaticDispatcherDiscovery().EnableAutomaticHandlerDiscovery().EnableHandlerServicesDescriptor().AddMediator<Mediator>());

            services.AddHostedService<LocationForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Savvyio_Example v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreFizzBuzzApi.Data;
using NetCoreFizzBuzzApi.Services;

namespace NetCoreFizzBuzzApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddCors(opts => opts.AddDefaultPolicy(builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
            
            RegisterDependencies(services);
        }

        public void RegisterDependencies(IServiceCollection services){
            services
                .AddSingleton<IAppInfoService, AppInfoService>()
                .AddSingleton<IRedisClientFactory>(RedisClientFactory.Instance);

            services
                .AddScoped<IFizzBuzzer, FizzBuzzer>()
                .AddScoped<ICounter, RedisCounter>(serviceProvider 
                    => new RedisCounter(
                        serviceProvider.GetService<IRedisClientFactory>(), 
                        Environment.GetEnvironmentVariable("FIZZBUZZ_REDIS_COUNTER_KEY"))
                    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

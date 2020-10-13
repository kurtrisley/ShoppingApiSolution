using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ShoppingApi.Domain;
using ShoppingApi.Profiles;

namespace ShoppingApi
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
            services.AddLogging();
            services.AddControllers();
            services.AddDbContext<ShoppingDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("shopping"))
            );

            //var mapperConfiguration = Configuration.GetValue<ConfigurationForMapper>("Mapper");

            var configForMapper = new ConfigurationForMapper();
            Configuration.GetSection(configForMapper.SectionName).Bind(configForMapper);

            // This sets up an IOptions<ConfigurationForMapper> that we can inject into other dependencies.
            services.Configure<ConfigurationForMapper>(Configuration.GetSection(configForMapper.SectionName));

            services.Configure<ConfigurationForMapper>(Configuration.GetSection("Mapper"));
            var mapperConfig = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new CatalogProfile(configForMapper));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
            services.AddSingleton<MapperConfiguration>(mapperConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

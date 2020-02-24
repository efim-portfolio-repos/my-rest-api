using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MyRestApi.Infrastructure.Repositories;
using MyRestApi.Domain.UseCases;

using AutoMapper;

using MyRestApi.Infrastructure;

namespace MyRestApi
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
      string dbConnectionString = Configuration.GetConnectionString("MyRestApi");

      services.AddTransient<IProductRepository, ProductRepository>();
      services.AddTransient<IProductUseCase, ProductUseCase>();

      services.AddDbContext<MyRestApiContext>(options =>
          options.UseNpgsql(dbConnectionString));

      services.AddAutoMapper(typeof(Startup));

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      var scoppedFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
      using (var scope = scoppedFactory.CreateScope())
      {
        MyRestApiContext
          .Seed(scope.ServiceProvider)
          .Wait();
      }
    }
  }
}

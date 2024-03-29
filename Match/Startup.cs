﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Match.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Match.Entities;
using Match.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Match.Services;

namespace Match
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
            //services.AddSingleton(_ => Configuration);

            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            //    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.IncludeIgnoredWarning)));

            //services.AddScoped<DbContext, AppDbContext>();
 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            Ioc.RegisterInheritedTypes(typeof(ServiceBase).Assembly, typeof(ServiceBase));

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddScoped<IMemberService, MemberService>();
            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

using AutoMapper;
using Changelog.Abstraction.Abstractions;
using Changelog.Business;
using Changelog.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;

namespace ChangelogAPI
{
    public static class ServiceExtensions
    {

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(op =>
            {
                op.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChangelogContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }


        public static void configureService(this IServiceCollection services)
        {
            services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            services.AddScoped<IUserRegistrationStorage, UserRegistrationRepository>();
            services.AddScoped<ILogsService, LogsService>();
            services.AddScoped<ILogsStorage, LogStorage>();
            services.AddAutoMapper(typeof(TaxInfo.EFCore.Mapper.ContextEntityMapper).Assembly);
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>

                {
                    int responseCode = context.Response.StatusCode;
                    if (responseCode == (int)HttpStatusCode.BadRequest)
                    {

                    }
                    return Task.FromResult(0);
                });
                await next();
            });
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error");
                    context.Response.AddApplicationError();
                });
            });
        }

        private static void AddApplicationError(this HttpResponse resp)
        {
            resp.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}

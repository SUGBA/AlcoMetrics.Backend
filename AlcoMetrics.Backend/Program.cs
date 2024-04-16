using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApp.Extensions;
using WebApp.Services.AccountServices;
using WebApp.Services.AccountServices.Abstract;
using WebApp.Services.API.IdentityApi;
using WebApp.Services.API.IdentityApi.Abstract;

namespace AlcoMetrics.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //Описание с AddJwtBearer для поддержки IdenittyModel 7.0
                options.Authority = builder.Configuration.TryGetValue("AuthSetting:ShareSettings:AuthenticationServicePath", "Конфиги не содержат домен IdentityServer");
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidAudiences = new List<string>() {
                    builder.Configuration.TryGetValue("AuthSetting:IdentitySettings:ApiName", "Конфиги не содержат наименование API для IdentityServer"),
                    builder.Configuration.TryGetValue("AuthSetting:IdentitySettings:ApiSecret", "Конфиги не содержат секрет API для IdentityServer"),
                    JwtClaimTypes.Role
            };
            });

            builder.Services.AddControllers();

            builder.Services.AddTransient<IIdentityApiService, IdentityApiService>();
            builder.Services.AddTransient<IAccountService, AccountService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}

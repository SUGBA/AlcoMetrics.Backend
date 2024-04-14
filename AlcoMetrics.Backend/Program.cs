using IdentityServer4.AccessTokenValidation;
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

            builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = builder.Configuration.TryGetValue("AuthSetting:ShareSettings:GetTokenPath", "Конфиги не содержат домен IdentityServer");
                options.RequireHttpsMetadata = false;
                options.ApiName = builder.Configuration.TryGetValue("AuthSetting:IdentitySettings:ApiName", "Конфиги не содержат наименование API для IdentityServer");
                options.ApiSecret = builder.Configuration.TryGetValue("AuthSetting:IdentitySettings:ApiSecret", "Конфиги не содержат секрет API для IdentityServer");
            });

            builder.Services.AddControllers();

            builder.Services.AddScoped<IIdentityApiService, IdentityApiService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

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

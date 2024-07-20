using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using TodoApp.Application;
using TodoApp.EntityFrameworkCore;
using Volo.Abp.Account;
using AccountAppService = TodoApp.Application.AccountAppService;

namespace TodoApp.Web;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
             .WriteTo.Console()
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logsMojde.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            builder.Services.AddDbContext<TodoAppDbContext>
                (options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString
                    ("Default"
                    )));
            builder.Services.AddTransient<IAccountAppService,
                AccountAppService>();
            builder.Services.AddAuthentication(
                options =>
                {
options.DefaultAuthenticateScheme=JwtBearerDefaults.
                    AuthenticationScheme;
                    options.DefaultChallengeScheme=
                    JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new
                    TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration
                        ["Jwt:issuer"],
                        ValidAudience = builder.Configuration
                        ["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]
                            ))
                    };
                }
                );
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("reguirelogin", policy =>
                policy.RequireAuthenticatedUser());
            }
            );
            builder.Services.AddEndpointsApiExplorer();
            await builder.AddApplicationAsync<TodoAppWebModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()  // Add this line
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
            });
}

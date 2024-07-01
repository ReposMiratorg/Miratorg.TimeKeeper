using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Miratorg.Common.Extensions;
using Miratorg.DataService.Extensions;
using Miratorg.DataService.Interfaces;
using Miratorg.DataService.Services;
using Miratorg.TimeKeeper.BusinessLogic.Configs;
using Miratorg.TimeKeeper.BusinessLogic.Services;
using Miratorg.TimeKeeper.Host.Controllers;
using System.Security.Claims;

namespace Miratorg.TimeKeeper.Host.Extensions;

public static class ServiceHostExtensions
{
    public const string CROSPolicy = "DefaultCorsPolicy";

    internal static void AddHostComponents(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(ApiController).Assembly);

        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: CROSPolicy, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        services.AddHttpClient();
        services.AddControllers();
        services.AddRazorPages();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        services.AddServerSideBlazor().AddCircuitOptions(o => o.DetailedErrors = true);

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });

        services.AddHttpContextAccessor();
        services.AddScoped<AuthenticationStateProvider, CustomTokenAuthenticationStateProvider>();

        services.AddLdapService();
        services.AddStaffControlDbContext();

        services.Configure<BlockingConfig>(configuration.GetSection(nameof(BlockingConfig)));
        services.AddSingleton<IBlockingService, BlockingService>();
        services.AddSingleton<IStuffControlDbService, StuffControlDbService>();
        services.AddSingleton<ISigurService, SigurService>();
        services.AddHostedService<SyncEmployeeService>();

        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IApiService, ApiService>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<TimeKeeperDbContext>(options =>
        {
            options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        services.AddSingleton<ITimeKeeperDbContextFactory, TimeKeeperDbContextFactory>();

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();

            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
        });
    }

    internal static void ConfigureApp(this WebApplication app)
    {
        app.UseForwardedHeaders();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            //app.UseHttpsRedirection();
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();
        app.UseCors(CROSPolicy);
        app.UseCertificateForwarding();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages().RequireAuthorization();
            endpoints.MapBlazorHub().RequireAuthorization();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
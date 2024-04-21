using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Miratorg.Common.Extensions;
using Miratorg.TimeKeeper.DataAccess.Contexts;
using System.Security.Claims;

namespace Miratorg.TimeKeeper.Host.Extensions;

public static class ServiceHostExtensions
{
    public const string CROSPolicy = "DefaultCorsPolicy";

    internal static void AddHostComponents(this IServiceCollection services)
    {
        services.AddControllers();

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

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<TemplateDbContext>(options =>
        {
            options.UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        services.AddSingleton<ITemplateDbContextFactory, TemplateDbContextFactory>();
        
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
            //endpoints.MapFallbackToPage("/Components/Pages/_Host");
        });
    }
}
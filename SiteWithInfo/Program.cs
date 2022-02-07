using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SiteWithInfo.Services;
using SiteWithInfo.Services.Interfaces;
using SiteWithInfo.Utils;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events.OnValidatePrincipal = async principal =>
        {
            if (long.TryParse(principal.Principal.FindFirstValue(MyClaims.SecurityStamp), out var timeStamp) &&
                Guid.TryParse(principal.Principal.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                var usersManager = principal.HttpContext.RequestServices.GetRequiredService<IUsersManager>();
                var user = await usersManager.GetUserByIdAsync(userId);
                if (user is not null && user.SecurityStamp == timeStamp)
                {
                    return;
                }
            }
            
            principal.RejectPrincipal();
            await principal.HttpContext.SignOutAsync();
        };
    });
builder.Services.AddAuthentication();

builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddScoped<ISignInManager, SignInManager>();
builder.Services.AddSingleton<IUsersManager, UserManagerForTests>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasherSha512>();
builder.Services.AddSingleton<IMessageManager, MessageManager>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
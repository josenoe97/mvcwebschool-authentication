using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using MvcWebIdentity.Services;
using MvcWebSchool_Identity.Data;
using MvcWebSchool_Identity.Policies;
using MvcWebSchool_Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("WebSchoolConnection");

builder.Services.AddDbContext<WebSchoolContext>(opts =>
opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<WebSchoolContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = "AspNetCore.Cookies";
        opts.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        opts.SlidingExpiration = true;
    });

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequiredLength = 10;
    opts.Password.RequiredUniqueChars = 3;
    opts.Password.RequireNonAlphanumeric = false;
});

// substitui [Authorize(Roles = "User, Admin, Gerente")]
//(RequireRole): é usado para edigir que um usuário tenha uma role específica para acessar um recurso
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("RequireUserAdminGerenteRole",
        policy => policy.RequireRole("User", "Admin", "Gerente"));
});

//(RequireClaim): especifica que o usuario precisa ter uma detemrinada declaração claim para acessar um recurso protegido
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("IsAdminClaimAccess",
        policy => policy.RequireClaim("CadastradoEm"));

    opts.AddPolicy("IsAdminClaimAccess",
        policy => policy.RequireClaim("IsAdmin", "true"));

    opts.AddPolicy("IsFuncionarioClaimAccess",
       policy => policy.RequireClaim("IsFuncionario", "true"));

    opts.AddPolicy("TempoCadastroMinimo", policy =>
        {
            policy.Requirements.Add(new TempoCadastroRequirement(5)); // tempo de cadastro minimo
        });
});

builder.Services.AddScoped<IAuthorizationHandler, TempoCadastroHandler>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<ISeedUserClaimsInitial, SeedUserClaimsInitial>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

await CriarPerfisUsuariosAsync(app);

//Identity // A ORDEM IMPORTA UseAuthentication();, DEPOIS UseAuthorization();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "MinhaArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task CriarPerfisUsuariosAsync(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
       /* var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        await service.SeedRolesAsync();
        await service.SeedUsersAsync();*/

        var service = scope.ServiceProvider.GetService<ISeedUserClaimsInitial>();
        await service.SeedUserClaims();
    }
}
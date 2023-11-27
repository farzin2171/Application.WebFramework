using Application.Data.Account;
using Application.Data.Database;
using Application.Domain.Settings;
using Application.Services.EmailServices;
using Application.Services.QRCode;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WT.WebApplication.Infrastructure.Extentions;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext();
});

builder.Services.AddRazorPages();

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "WT.WebApplication";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthentication().AddCookie("temp")
    .AddGoogle("Google", o =>
    {
        o.ClientId = "635672167628-qc09tqq0rhe4hlf431ivqp5g30ab1e4r.apps.googleusercontent.com";
        o.ClientSecret = "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";
        o.SignInScheme = "temp";

    });

builder.Services.Configure<SmtpSetting>(builder.Configuration.GetSection("SMTP"));
builder.Services.Configure<ExternalGoogle>(builder.Configuration.GetSection("GoogleExtensions"));

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IQRCodeService, QRCodeService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSerilogRequestLogging();


app.MapRazorPages();
app.MapControllers();

app.RunDatabaseMigrations<ApplicationDbContext>();

app.Run();

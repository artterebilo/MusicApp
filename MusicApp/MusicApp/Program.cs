using DataBase.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using MusicApp;
using MusicApp.Contracts;
using MusicApp.Services;
using MusicApp.Validations;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(opts =>
{
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IValidator<UserCreateContract>, UserCreateValidation>();
builder.Services.AddTransient<IValidator<UserUpdateContract>, UserUpdateValidation>();
builder.Services.AddTransient<IValidator<UserLikeSongContract>, UserLikeValidation>();
builder.Services.AddTransient<IValidator<LikePagination>, LikePaginationValidation>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Constants.Issuer,
                ValidAudience = Constants.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey)),
            };
        });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAuthenticatedUser", policy =>
//        policy.RequireAuthenticatedUser());
//});

//builder.Services.AddSwaggerGen();

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

app.MapControllers();

var supportedCultures = new[]
{
                new CultureInfo("en"),
                new CultureInfo("ru"),
            };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});


app.Run();

using EcoBazzar.BindingModel.User;
using EcoBazzar.DataBase;
using EcoBazzar.Services.CategoryServices;
using EcoBazzar.Services.ProductServices;
using EcoBazzar.Services.SubCategoryServices;
using EcoBazzar.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


// Define a secret key for JWT
string secretKey = "yourVeryLongSecretKey12345"; // This should be at least 16 characters long
string issuer = "EcoBazzar";
string audience = "https://localhost:7106";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();

//
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

//adding connection string
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
// REST API


///////User API ///////

app.MapGet("/api/user", [Authorize] async (IUserServices services) =>
{
    var users = await services.GetAllUsers();
    return Results.Ok(users);
});

app.MapGet("/api/user/{id}", [Authorize] async (IUserServices services, int id) =>
{
    var user = await services.GetUser(id);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
});

app.MapPost("/api/user", [Authorize] async (IUserServices services, UserBindingModel bindingModel) =>
{
    var id = await services.CreateUSer(bindingModel);
    return Results.Created($"api/user/{id}", bindingModel);
});

app.MapDelete("/api/user", [Authorize] async (IUserServices services, int id) =>
{
    var x = await services.DeleteUser(id);
    if (x)
    {
        return Results.Ok("Deleted Successfully");
    }
    return Results.NotFound();
});



app.MapPost("/api/authenticate", async (IUserServices services, AuthRequest authRequest) =>
{
    var user = await services.AuthenticateUser(authRequest.Username, authRequest.Password);
    if (user == null)
    {
        return Results.Unauthorized();
    }

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(secretKey);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);

    return Results.Ok(new { Token = tokenString });
});


///////////////////////


//
app.Run();




public class AuthRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
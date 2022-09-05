
using Account.Data;
using Account.Data.EF;
using Account.Service;
using Linq2DynamoDb.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DataContext>();

builder.Services.AddDbContextFactory<AccountDbContext>(item =>
    item.UseSqlServer(builder.Configuration.GetConnectionString("myconn")));


// Add services to the container.
builder.Services.AddScoped<IAccountData, AccountData>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoginData, LoginData>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//using (var client = new AccountDbContext())
//{
//    client.Database.EnsureCreated();
//}
//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
//    dataContext.Database.Migrate();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

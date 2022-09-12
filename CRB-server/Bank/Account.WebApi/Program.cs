using Account.Service;
using Account.WebApi.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ExtensionsDI();
builder.Services.ExtensionAddDbContext(builder.Configuration.GetConnectionString("myconn"));


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

//app.UseErrorHandlerMiddleware();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

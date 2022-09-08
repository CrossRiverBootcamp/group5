using Microsoft.Data.SqlClient;
using NServiceBus;
using Transaction.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ExtensionsDI();
builder.Services.ExtensionAddDbContext(builder.Configuration.GetConnectionString("myconn"));


var rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQ");
var databaseConnection = builder.Configuration.GetConnectionString("myconn");
var databaseNSBConnection = builder.Configuration.GetConnectionString("NSB");

#region back-end-use-nservicebus
builder.Host.UseNServiceBus(hostBuilderContext =>
{

    var endpointConfiguration = new EndpointConfiguration("TransactionApi");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();
    endpointConfiguration.SendOnly();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(databaseNSBConnection);
    });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("NSB");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    return endpointConfiguration;
});

#endregion

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

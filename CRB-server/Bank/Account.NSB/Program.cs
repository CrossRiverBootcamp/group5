﻿using Account.Data;
using Account.Service;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;

class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task Main()
    {
        Console.Title = "Account";
        var endpointConfiguration = new EndpointConfiguration("Account");
        var databaseConnection = "Server=DESKTOP-R5RADSP;Database=TrackingDB;Trusted_Connection=True;";
        //var databaseConnection = "Server=DESKTOP-8AHFHCN;Database=TrackingDB;Trusted_Connection=True;";
        var rabbitMQConnection = @"host=localhost";
        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<IAccountService, AccountService>();
        containerSettings.ServiceCollection.AddScoped<IAccountData, AccountData>();
        //containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
        //containerSettings.ServiceCollection.AddDbContextFactory<MeasureContext>(opt => opt.UseSqlServer(databaseConnection));
       
        #region ReceiverConfiguration
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(databaseConnection);
            });
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");
        var conventions = endpointConfiguration.Conventions();
        //conventions.DefiningEventsAs(type => type.Namespace == "Measure.Messages.Events");
        #endregion
       
        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        Console.WriteLine("waiting for messages...");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}

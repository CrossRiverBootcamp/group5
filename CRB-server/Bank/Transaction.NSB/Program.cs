using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Data;
using Transaction.Service;

class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "Transaction";

        var endpointConfiguration = new EndpointConfiguration("Transaction");

        var databaseConnection = "Server=DESKTOP-8AHFHCN;Database=TransactionDB;Trusted_Connection=True;";
        var rabbitMQConnection = @"host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<ITransactionService, TransactionService>();
        containerSettings.ServiceCollection.AddScoped<ITransactionData, TransactionData>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
        //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        containerSettings.ServiceCollection.ExtensionAddDbContext(databaseConnection);

        #region ReceiverConfiguration
        var databaseNSBConnection = "Server=DESKTOP-8AHFHCN;Database=BankNSB;Trusted_Connection=True;";
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(databaseNSBConnection);
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("NSB");

        //var conventions = endpointConfiguration.Conventions();
        //conventions.DefiningEventsAs(type => type.Namespace == "Measure.Messages.Events");
        #endregion 

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}
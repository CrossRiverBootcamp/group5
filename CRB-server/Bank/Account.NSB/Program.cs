
class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task Main()
    {
        Console.Title = "Account";
        var endpointConfiguration = new EndpointConfiguration("Account");

        //var databaseConnection = "Server=DESKTOP-8AHFHCN;Database=Bank;Trusted_Connection=True;";
        var databaseConnection = "Server=DESKTOP-R5RADSP;Database=Bank;Trusted_Connection=True;";
        var rabbitMQConnection = @"host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<IAccountService, AccountService>();
        containerSettings.ServiceCollection.AddScoped<IAccountData, AccountData>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
        containerSettings.ServiceCollection.ExtensionAddDbContext(databaseConnection);

        #region ReceiverConfiguration
        //var databaseNSBConnection = "Server=DESKTOP-8AHFHCN;Database=BankNSB;Trusted_Connection=True;";
        var databaseNSBConnection = "Server=DESKTOP-R5RADSP;Database=BankNSB;Trusted_Connection=True;";

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
        #endregion

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);
        Console.WriteLine("waiting for messages...");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}

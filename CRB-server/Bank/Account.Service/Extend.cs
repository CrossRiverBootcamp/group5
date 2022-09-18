

namespace Account.Service;

public static class Extend
{
    public static void ExtensionsDI(this IServiceCollection service)
    {
        service.AddScoped<IAccountData, AccountData>();
        service.AddScoped<IAccountService, AccountService>();
        service.AddScoped<ILoginData, LoginData>();
        service.AddScoped<ILoginService, LoginService>();
        service.AddScoped<IOperationsHistoryService, OperationsHistoryService>();
        service.AddScoped<IOperationsHistoryData, OperationsHistoryData>();
    }
    public static void ExtensionAddDbContext(this IServiceCollection service, string Connection)
    {
        service.AddDbContextFactory<AccountDbContext>(item =>
            item.UseSqlServer(Connection));
    }
    public static void ExtensionMigrateDB(this IServiceCollection service, IServiceProvider appServices)
    {
        using (var scope = appServices.CreateScope())
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
            dataContext.Database.Migrate();
        }
    }
}

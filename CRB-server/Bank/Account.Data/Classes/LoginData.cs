
namespace Account.Data.Classes;

public class LoginData : ILoginData
{
    private readonly IDbContextFactory<AccountDbContext> _factory;
    public LoginData(IDbContextFactory<AccountDbContext> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public async Task<Guid> GetAccountIdAsync(string email, string password)
    {
        using var context = _factory.CreateDbContext();
        Entities.Account account = await context.Accounts.Include(account => account.Customer)
                .FirstOrDefaultAsync(account => account.Customer.Email.Equals(email)
                                        && account.Customer.Password.Equals(password));
        return account == null ? Guid.Empty: account.Id;        
    }
}

﻿
namespace Account.Data.Classes;

public class LoginData : ILoginData
{
    private readonly IMapper _mapper;

    private readonly IDbContextFactory<AccountDbContext> _factory;
    public LoginData(IDbContextFactory<AccountDbContext> factory, IMapper mapper)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _mapper = mapper;
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

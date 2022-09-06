using Account.Data.EF;
using Account.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
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
            using (var context = _factory.CreateDbContext())
            {
                Entities.Account account = await context.Accounts.Include(account => account.Customer)
                    .FirstOrDefaultAsync(account => account.Customer.Email.Equals(email)
                                            && account.Customer.Password.Equals(password));
                if (account == null)
                    return Guid.Empty;
                return account.Id;
            }
        }

        public async Task<CustomerInfoModel> GetCustomerInfoAsync(Guid accountId)
        {
            using (var context = _factory.CreateDbContext())
            {
                Entities.Account account = await context.Accounts.Include(account => account.Customer)
                    .FirstOrDefaultAsync(account => account.Id.Equals(accountId));
                if (account == null)
                    return null;
                CustomerInfoModel customerInfoModel = _mapper.Map<CustomerInfoModel>(account.Customer);
                customerInfoModel.Balance = account.Balance;
                return customerInfoModel;

            }
        }


    }
}

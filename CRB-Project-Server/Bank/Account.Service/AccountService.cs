using Account.Data;
using Account.Data.Entities;
using Account.Service.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountData _accountData;
        private readonly IMapper _mapper;

        public AccountService(IAccountData accountData, IMapper mapper)
        {
            _accountData = accountData;
            _mapper = mapper;
        }
        public async Task<bool> AddCustomerAsync(CustomerDTO customerDTO)
        {
            if (await _accountData.IsEmailExistAsync(customerDTO.Email))
            {
                Customer customer = _mapper.Map<Customer>(customerDTO);
                bool isCustomerAdded=await _accountData.AddCustomerAsync(customer);
                Data.Entities.Account account = new Data.Entities.Account()
                {
                    CustomerId = customer.Id,
                    OpenDate = DateTime.UtcNow,
                    Balance=1000
                };
                bool isAccountCreated=await _accountData.CreateAccountAsync(account);
                if(isCustomerAdded&& isAccountCreated)
                    return true;
                return false;
            }
            return false;
        }

        
    }
}

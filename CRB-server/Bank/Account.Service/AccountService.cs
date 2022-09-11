using Account.Data;
using Account.Data.Entities;
using Account.Service.DTO;
using AutoMapper;
using NSB.Messages;
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


        public Task<bool> DoesAccountExist(Guid accountId)
        {
            return _accountData.DoesAccountExist(accountId);
        }

        public Task<bool> IsBalanceGreater(Guid accountId, int amount)
        {
            return _accountData.IsBalanceGreater(accountId, amount);
        }
        public Task<bool> TransactionBetweenAccountsAsync(Guid fromAccountId, Guid toAccountId, int amount)
        {
            return _accountData.TransactionBetweenAccountsAsync(fromAccountId, toAccountId, amount);
        }
        //operationservice in diffrent page?
        public async Task<bool> AddOperation(MakeTransfer makeTransfer)
        {
            //add model?
            Operation operationFromAccount = _mapper.Map<Operation>(makeTransfer);
            operationFromAccount.AccountId = makeTransfer.FromAccountID;
            operationFromAccount.Debit_Credit = false;
            operationFromAccount.Balance = await _accountData.GetBalanceByAccountIdAsync(operationFromAccount.AccountId);
            operationFromAccount.OperationTime = DateTime.UtcNow;

            Operation operationToAccount = _mapper.Map<Operation>(makeTransfer);
            operationToAccount.AccountId = makeTransfer.ToAccountID;
            operationToAccount.Debit_Credit = true;
            operationToAccount.Balance = await _accountData.GetBalanceByAccountIdAsync(operationToAccount.AccountId);
            operationToAccount.OperationTime = DateTime.UtcNow;

            return await _accountData.AddOperation(operationFromAccount, operationToAccount);
        }
    }
}

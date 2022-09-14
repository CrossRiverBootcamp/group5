﻿using Account.Data.Entities;
using Account.Data.Interfaces;
using Account.Service.DTO;
using Account.Service.Interfaces;
using Account.Services;
using AutoMapper;
using NSB.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Account.Service.Classes
{
    public class AccountService : IAccountService
    {
        private readonly IAccountData _accountData;
        private readonly IMapper _mapper;

        public AccountService(IAccountData accountData, IMapper mapper)
        {
            _accountData = accountData;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();

        }

        public async Task<bool> CreateVerificationCode(string email)
        {
            if (!await _accountData.IsEmailExistAsync(email))
            {
                int code = new Random().Next(1000, 10000);

                EmailVerification emailVerification = new EmailVerification()
                {
                    Email = email,
                    VerificationCode = code,
                    ExpirationTime = DateTime.Now.AddMinutes(30)
                };
                if (await _accountData.CreateEmailVerification(emailVerification))
                {
                    var fromAddress = new MailAddress("crbcrproject@gmail.com", "CRB C&R");
                    var toAddress = new MailAddress(email);
                    const string fromPassword = "nbitifwvunfkzyoa";

                    const string subject = "Verification code";
                    string body = "Hi We received a request to create a bank account for you" +
                        " Your verification code  from Cross River Bank is: " + code 
                        + ". this verification code will expire in 30 minutes!";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                        Timeout = 5000,
                        UseDefaultCredentials = false
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                    return true;
                }
                return false;//?
            }
            return false;//?
        }

        public async Task<bool> AddCustomerAsync(CustomerDTO customerDTO)
        {

            Customer customer = _mapper.Map<Customer>(customerDTO);
            bool isCustomerAdded = await _accountData.AddCustomerAsync(customer);
            Data.Entities.Account account = new Data.Entities.Account()
            {
                CustomerId = customer.Id,
                OpenDate = DateTime.UtcNow,
                Balance = 1000
            };
            bool isAccountCreated = await _accountData.CreateAccountAsync(account);
            if (isCustomerAdded && isAccountCreated)
                return true;
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

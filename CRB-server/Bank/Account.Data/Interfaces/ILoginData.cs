﻿
namespace Account.Data.Interfaces;

public interface ILoginData
{
    Task<Guid> GetAccountIdAsync(string email, string password);
}
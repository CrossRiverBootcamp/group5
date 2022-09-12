using Account.Data.Models;

namespace Account.Data.Interfaces
{
    public interface ILoginData
    {
        Task<Guid> GetAccountIdAsync(string email, string password);
        Task<CustomerInfoModel> GetCustomerInfoAsync(Guid accountId);
    }
}
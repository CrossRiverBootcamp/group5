
namespace Account.Service.Interfaces;

public interface ILoginService
{
    Task<Guid> Login(LoginDTO loginDTO);
}
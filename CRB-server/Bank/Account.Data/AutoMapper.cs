
namespace Account.Data;

public class AutoMapper: Profile
{
    public AutoMapper()
    {
        CreateMap<Customer, CustomerInfoModel>();
    }
}

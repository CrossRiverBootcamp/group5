
namespace Transaction.NSB;

public class AutoMapper:Profile
{
    public AutoMapper()
    {
        CreateMap<TransactionAdded, MakeTransfer>();
    }
}


namespace Transaction.Services;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TransactionDTO, Data.Entities.Transaction>();
        CreateMap<TransactionDTO, TransactionAdded>();
        CreateMap<Transfered, UpdateTransactionModel>();
    }
}


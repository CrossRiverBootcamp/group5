
namespace Account.Services;
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerDTO, Customer>();

            CreateMap<Data.Entities.Account, AccountInfoDTO>()
            .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(src => src.Customer.FirstName))
            .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.Customer.LastName))
             .ForMember(dest => dest.Email,
                            opt => opt.MapFrom(src => src.Customer.Email)); 

            CreateMap<MakeTransfer, Operation>()
            .ForMember(dest => dest.TransactionAmount,      
                            opt => opt.MapFrom(src => src.Amount));

            CreateMap<Operation, OperationDTO>()
            .ForMember(dest => dest.Amount,
                            opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest => dest.Date,
                            opt => opt.MapFrom(src => src.OperationTime));

            CreateMap<Data.Entities.Account, CustomerInfoDTO>()
            .ForMember(dest => dest.FirstName,
                            opt => opt.MapFrom(src => src.Customer.FirstName))
            .ForMember(dest => dest.LastName,
                            opt => opt.MapFrom(src => src.Customer.LastName))
            .ForMember(dest => dest.Email,
                            opt => opt.MapFrom(src => src.Customer.Email));

    }
    }

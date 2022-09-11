using Account.Data.Entities;
using Account.Data.Models;
using Account.Service.DTO;
using AutoMapper;
using NSB.Messages;

namespace Account.Services; 
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<CustomerDTO, Customer>();
        CreateMap<CustomerInfoModel, CustomerInfoDTO>();
        CreateMap<MakeTransfer, Operation>()
        .ForMember(dest => dest.TransactionAmount,
                        opt => opt.MapFrom(src => src.Amount));
        CreateMap<Operation, OperationDTO>()
            .ForMember(dest => dest.Amount,
                        opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(dest => dest.Date,
                        opt => opt.MapFrom(src => src.OperationTime));
    }
}


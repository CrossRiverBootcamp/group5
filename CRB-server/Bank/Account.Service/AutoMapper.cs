using Account.Data.Entities;
using Account.Data.Models;
using Account.Service.DTO;
using AutoMapper;
using NSB.Messages;

namespace Account.Services;
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<CustomerInfoModel, CustomerInfoDTO>();
            CreateMap<MakeTransfer, Operation>()
            .ForMember(dest => dest.TransactionAmount,      
                            opt => opt.MapFrom(src => src.Amount));
        }
    }


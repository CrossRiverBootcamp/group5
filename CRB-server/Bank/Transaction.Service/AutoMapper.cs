
using AutoMapper;
using NSB.Messages;
using Transaction.Service.DTO;

namespace Transaction.Services;
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<TransactionDTO, Data.Entities.Transaction>();
            CreateMap<TransactionDTO, TransactionAdded>();
    }
    }


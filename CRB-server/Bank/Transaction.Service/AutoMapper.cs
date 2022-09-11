
using AutoMapper;
using NSB.Messages;
using Transaction.Service.DTO;
using Transaction.Service.Models;

namespace Transaction.Services;
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TransactionDTO, Data.Entities.Transaction>();
            CreateMap<TransactionDTO, TransactionAdded>();
            CreateMap<Transfered, UpdateTransactionModel>();
        }
    }


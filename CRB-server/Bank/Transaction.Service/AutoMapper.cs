
using AutoMapper;
using NSB.Messages;
using Transaction.Service.DTO;
using Transaction.Service.Models;

namespace Transaction.Services;
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<TransactionDTO, Data.Entities.Transaction>();
            CreateMap<TransactionDTO, TransactionAdded>();
            CreateMap<Transfered, UpdateTransactionModel>();
        }
    }


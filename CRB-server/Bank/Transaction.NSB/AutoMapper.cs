using AutoMapper;
using NSB.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Service.Models;

namespace Transaction.NSB
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Transfered, UpdateTransactionModel>();
            CreateMap<TransactionAdded, MakeTransfer>();

        }
    }
}

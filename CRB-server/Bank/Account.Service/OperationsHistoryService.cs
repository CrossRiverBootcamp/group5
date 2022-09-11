using Account.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public class OperationsHistoryService
    {
        private readonly IOperationsHistoryData _OperationsHistoryData;
        private readonly IMapper _mapper;

        public OperationsHistoryService(IOperationsHistoryData OperationsHistoryData, IMapper mapper)
        {
            _OperationsHistoryData = OperationsHistoryData;
            _mapper = mapper;
        }
        public async Task<List<Data.Entities.Account>> GetOperationsHistotyListAsync(Guid AccountID)
        {
            Task<List<Data.Entities.Account>> operationsList = await _OperationsHistoryData.GetOperationsHistoty(AccountID).ToListAsync();
        }
    }
}

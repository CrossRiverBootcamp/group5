using Account.Data;
using Account.Data.Entities;
using Account.Service.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public class OperationsHistoryService : IOperationsHistoryService
    {
        private readonly IOperationsHistoryData _OperationsHistoryData;
        private readonly IMapper _mapper;

        public OperationsHistoryService(IOperationsHistoryData OperationsHistoryData, IMapper mapper)
        {
            _OperationsHistoryData = OperationsHistoryData;
            _mapper = mapper;
        }
        public async Task<List<OperationDTO>> GetOperationsHistotyListAsync(Guid AccountID)
        {
            List<Operation> operationsList = await _OperationsHistoryData.GetOperationsHistoty(AccountID);
            List<OperationDTO> operationsListDTO = operationsList.ConvertAll(operation => _mapper.Map<OperationDTO>(operation));
            return operationsListDTO;
        }
    }
}

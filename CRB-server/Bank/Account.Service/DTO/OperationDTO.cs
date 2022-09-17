using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service.DTO;

public class OperationDTO
{
    public Guid AccountID { get; set; }
    public bool Debit_Credit { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    public DateTime Date { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.Entities;

public class Operation
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public Guid AccountId { get; set; }
    [Required]
    public Guid TransactionId  { get; set; }
    [Required]
    public Boolean Debit_Credit { get; set; }
    [Required]
    public int TransactionAmount { get; set; }
    [Required]
    public int Balance { get; set; }
    [Required]
    public DateTime OperationTime { get; set; }

}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data.EF;

public class TransactionDbContext : DbContext
{
    public TransactionDbContext()
    {

    }
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Entities.Transaction> Transactions { get; set; }

}

using Account.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.EF;

public class AccountDbContext : DbContext
{
    public AccountDbContext()
    {

    }
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Entities.Account> Accounts { get; set; }
    public virtual DbSet<Operation> Operations { get; set; }
    public virtual DbSet<EmailVerification> EmailVerifications { get; set; }

}

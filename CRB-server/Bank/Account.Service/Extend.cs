﻿using Account.Data;
using Account.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public static class Extend
    {
        public static void ExtensionsDI(this IServiceCollection service)
        {
            service.AddScoped<IAccountData, AccountData>();
            service.AddScoped<IAccountService, AccountService>();
            service.AddScoped<ILoginData, LoginData>();
            service.AddScoped<ILoginService, LoginService>();
        }
        public static void ExtensionAddDbContext(this IServiceCollection service, string Connection)
        {
            service.AddDbContextFactory<AccountDbContext>(item =>
                item.UseSqlServer(Connection));
        }
    }
}

using Account.Data.Entities;
using Account.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public class AutoMapper: Profile
    {
        public AutoMapper()
        {
            CreateMap<Customer, CustomerInfoModel>();
        }
    }
}

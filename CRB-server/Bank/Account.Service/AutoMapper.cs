﻿using Account.Data.Entities;
using Account.Data.Models;
using Account.Service.DTO;
using AutoMapper;


namespace Subscriber.Services;
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<CustomerDTO, Customer>();
        CreateMap<CustomerInfoModel, CustomerInfoDTO>();
        }
    }

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaTime.Api.DTOS;
using TeaTime.Entities;

namespace TeaTime.Api.TeaTimeMapper
{
    public class TeaTimeMapping:Profile
    {
        public TeaTimeMapping()
        {
            CreateMap<NationalPark, NationalParkDtos>().ReverseMap();
        }
    }
}

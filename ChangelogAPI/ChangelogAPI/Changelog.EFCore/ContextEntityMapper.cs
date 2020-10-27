using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Changelog.Abstraction.DTOs;
using Changelog.EFCore.Model;

namespace TaxInfo.EFCore.Mapper
{
    public class ContextEntityMapper : Profile
    {
        public ContextEntityMapper()
        {
            CreateMap<LogsContextDTO, LogsModel>().ReverseMap();
            CreateMap<LogTypesModel, LogTypeDTO>();
        }
    }
}

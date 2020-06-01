using AutoMapper;
using Stocky.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stocky.Application.Service
{
    public interface IBaseServiceInjector
    {
        TwinlineEntityContext _context { get; set; }
        IMapper _mapper { get; set; }
    }

    public class BaseServiceInjector : IBaseServiceInjector
    {
        public TwinlineEntityContext _context { get; set; }
        public IMapper _mapper { get; set; }

        public BaseServiceInjector(TwinlineEntityContext twinlineEntityContext, IMapper mapper)
        {
            _context = twinlineEntityContext;
            _mapper = mapper;
        }
    }
}


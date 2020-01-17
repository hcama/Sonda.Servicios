using AutoMapper;
using Sonda.Api.Resources;
using Sonda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sonda.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<TipoCliente, TipoClienteResource>();
           // CreateMap<Cliente, ArtistResource>();

            // Resource to Domain
            CreateMap<TipoClienteResource, TipoCliente>();
            //CreateMap<ArtistResource, Artist>();
        }
    }
}

using AutoMapper;
using msAlerta.Dto;
using msAlerta.Entity;

namespace msAlerta.Mapper
{
    public class AlertaProfile : Profile
    {
        public AlertaProfile() 
        {
            CreateMap<AlertaDto, Alerta>();
            CreateMap<Alerta, AlertaDto>();
        }
    }
}

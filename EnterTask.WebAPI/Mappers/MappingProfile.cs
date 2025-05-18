using AutoMapper;
using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.WebAPI.DTOs;

namespace EnterTask.WebAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventDTO, Event>().ReverseMap();
            CreateMap<EventFilterSettingDTO, EventFilterSettings>().ReverseMap();
            CreateMap<NotificationDTO, EventChange>().ReverseMap();
            CreateMap<ParticipantDTO, Participant>().ReverseMap();
            CreateMap<RegistrationDTO, Registration>().ReverseMap();
        }
    }
}

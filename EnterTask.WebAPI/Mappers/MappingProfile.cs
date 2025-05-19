using AutoMapper;
using EnterTask.Data.DataEntities;
using EnterTask.Data.FilterSettings;
using EnterTask.Data.Repository;
using EnterTask.WebAPI.DTOs;

namespace EnterTask.WebAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventDTO, Event>().ReverseMap();
            CreateMap<EventImageDTO, EventImage>().ReverseMap();
            CreateMap<NotificationDTO, EventChange>().ReverseMap();
            CreateMap<ParticipantDTO, Participant>().ReverseMap();
            CreateMap<RegistrationDTO, Registration>().ReverseMap();
            CreateMap<PersonDTO, Person>().ReverseMap();

            CreateMap<ParticipantGetSettingsDTO, PageInfo>();
            CreateMap<EventGetSettingsDTO, EventFilterSettings>();
            CreateMap<EventGetSettingsDTO, PageInfo>();
        }
    }
}

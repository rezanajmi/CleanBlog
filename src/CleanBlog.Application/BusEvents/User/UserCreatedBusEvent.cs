using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Identity;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.QueryEntities.User;
using AutoMapper;

namespace CleanBlog.Application.Events.User
{
    public class UserCreatedBusEvent : IBusEvent, IMapTo<UserQueryEntity>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }

        public UserCreatedBusEvent() { /* for mapping */ }

        public UserCreatedBusEvent(Entities.User user)
        {
            Id = user.Id;
            Username = user.UserName;
            Family = user.Family;
            Email = user.Email;
            Name = user.Name;
        }

        public void Mapping(Profile profile) =>
            profile.CreateMap<UserCreatedBusEvent, UserQueryEntity>()
            .ForMember(
                qe => qe.FullName,
                option => option.MapFrom(e => $"{e.Name} {e.Family}"))
            .ForMember(
                qe => qe.Id,
                option => option.MapFrom(e => e.Id.ToString()));
    }
}

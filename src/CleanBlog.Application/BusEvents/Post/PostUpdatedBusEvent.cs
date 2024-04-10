using CleanBlog.Application.Abstractions;
using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Post;
using CleanBlog.Application.QueryEntities.Post;
using AutoMapper;

namespace CleanBlog.Application.Events.Post
{
    public class PostUpdatedBusEvent : IBusEvent, IMapTo<PostQueryEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public PostUpdatedBusEvent() { /* for mapping */ }

        public PostUpdatedBusEvent(Entities.Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            UserId = post.UserId;
            UserFirstName = post.User.Name;
            UserLastName = post.User.Family;
            CategoryId = post.CategoryId;
            CategoryTitle = post.Category.Title;
        }

        public void Mapping(Profile profile) => profile.CreateMap<PostUpdatedBusEvent, PostQueryEntity>().ForMember(
                d => d.UserFullName,
                option => option.MapFrom(s => $"{s.UserFirstName} {s.UserLastName}")
            );
    }
}

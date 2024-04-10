using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Post;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.QueryEntities.Post;
using AutoMapper;

namespace CleanBlog.Application.Events.Post
{
    public class PostCreatedBusEvent : IBusEvent, IMapTo<PostQueryEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }

        public PostCreatedBusEvent() { /* for mapping */ }

        public PostCreatedBusEvent(Entities.Post post)
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

        public void Mapping(Profile profile) => profile.CreateMap<PostCreatedBusEvent, PostQueryEntity>().ForMember(
                qe => qe.UserFullName,
                option => option.MapFrom(be => $"{be.UserFirstName} {be.UserLastName}")
            );
    }
}

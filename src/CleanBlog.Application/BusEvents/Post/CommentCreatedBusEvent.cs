using AutoMapper;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.QueryEntities.Post;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.Entities.Post;

namespace CleanBlog.Application.Events.Post
{
    public class CommentCreatedBusEvent : IBusEvent, IMapTo<CommentQueryEntity>
    {
        public long Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public CommentCreatedBusEvent() { /* for mapping */ }

        public CommentCreatedBusEvent(Comment comment)
        {
            Id = comment.Id;
            PostId = comment.PostId;
            Text = comment.Text;
            UserFirstName = comment.User.Name;
            UserLastName = comment.User.Family;
        }

        public void Mapping(Profile profile) => profile.CreateMap<CommentCreatedBusEvent, CommentQueryEntity>().ForMember(
            qe => qe.UserFullName,
            option => option.MapFrom(be => $"{be.UserFirstName} {be.UserLastName}")
            );
    }
}

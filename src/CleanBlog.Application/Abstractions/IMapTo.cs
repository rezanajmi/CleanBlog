using AutoMapper;

namespace CleanBlog.Application.Abstractions
{
    public interface IMapTo<T> where T : class
    {
        void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}

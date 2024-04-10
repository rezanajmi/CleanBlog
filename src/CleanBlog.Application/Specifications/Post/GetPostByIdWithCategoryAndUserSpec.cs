using CleanBlog.Domain.Abstractions;
using Entities = CleanBlog.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;

namespace CleanBlog.Application.Specifications.Post
{
    internal sealed class GetPostByIdWithCategoryAndUserSpec : Specification<Entities.Post>
    {
        public GetPostByIdWithCategoryAndUserSpec(int id)
        {
            Query = q => q.Include(p => p.Category)
                .Include(p => p.User)
                .Where(p => p.Id == id);
        }
    }
}

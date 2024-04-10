using AutoMapper;
using CleanBlog.Application.Abstractions;
using CleanBlog.Application.Queries.Post;
using CleanBlog.Application.Results.Post;
using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Models;
using CleanBlog.Application.QueryEntities.Post;
using CleanBlog.Application.Exceptions;
using CleanBlog.Application.Specifications.Post;

namespace CleanBlog.Web.Queries.Posts.Handlers
{
    internal class PostQueriesHandler :
        IQueryHandler<GetPostByIdQuery, GetPostByIdResult>,
        IQueryHandler<GetPaginatedPostsQuery, PagedList<SearchedPostResult>>,
        IQueryHandler<GetPostCommentsQuery, IList<GetPostCommentsResult>>
    {
        protected IAsyncQueryRepository repository;
        protected IMapper mapper;
        public PostQueriesHandler(IAsyncQueryRepository repo, IMapper mapper)
        {
            this.repository = repo;
            this.mapper = mapper;
        }

        public async Task<GetPostByIdResult> Handle(GetPostByIdQuery request, CancellationToken ct)
        {
            var post = await repository.GetAsync<PostQueryEntity>(request.Id.ToString(), ct);
            if (post is null)
            {
                throw new ValidationException("Post id is wrong.");
            }
            return mapper.Map<GetPostByIdResult>(post);
        }

        public async Task<PagedList<SearchedPostResult>> Handle(GetPaginatedPostsQuery request, CancellationToken ct)
        {
            var repoResult = await repository.GetPagedListAsync<PostQueryEntity>(
                new SearchPostsByTitleSpec(request.SearchedTitle), request.Page, request.PageItemCount, ct);

            return new PagedList<SearchedPostResult>()
            {
                PagingInfo = repoResult.PagingInfo,
                List = repoResult.List.Select(x => mapper.Map<SearchedPostResult>(x)).ToList()
            };
        }

        public async Task<IList<GetPostCommentsResult>> Handle(GetPostCommentsQuery request, CancellationToken ct)
        {
            var post = await repository.GetAsync<PostQueryEntity>(request.PostId.ToString(), ct);
            if (post == null)
            {
                throw new ValidationException("PostId is wrong.");
            }

            var comments = await repository.GetListAsync<CommentQueryEntity>(new GetQueryCommentsByPostIdSpec(request.PostId), ct);

            var list = new List<GetPostCommentsResult>();
            if (comments.Any())
            {
                list.AddRange(comments.Select(x => mapper.Map<GetPostCommentsResult>(x)));
            }
            return list;
        }
    }
}

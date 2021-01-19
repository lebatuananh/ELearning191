using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Blog;
using PeaLearning.Common;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using PeaLearning.Domain.AggregateModels.TagAggregate;
using Shared.EF;
using Shared.Extensions;

namespace PeaLearning.Application.Write.BlogHandler
{
    public class AddBlogCommandHandler : IRequestHandler<AddBlogCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;

        public AddBlogCommandHandler(IUnitOfWork uow, IBlogRepository blogRepository, ITagRepository tagRepository)
        {
            _uow = uow;
            _blogRepository = blogRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Unit> Handle(AddBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = new Blog(request.Name, request.Thumbnail, request.Description, request.HomeFlag, request.HotFlag,
                request.SeoPageTitle, request.SeoAlias, request.SeoKeywords, request.SeoDescription, request.Status,
                request.Tags,request.Content);
            blog.BlogTags = new List<BlogTag>();
            if (!string.IsNullOrEmpty(request.Tags))
            {
                var tags = request.Tags.Split(',');
                foreach (string t in tags)
                {
                    var text = t.Trim();
                    var tagId = text.ToSlug();
                    if (!await _tagRepository.ExistsAsync(x => x.Id == tagId))
                    {
                        Tag tag = new Tag(tagId, t, Constants.Tag.BlogTag);
                        _tagRepository.Add(tag);
                    }

                    var blogTag = new BlogTag {TagId = tagId};
                    blog.BlogTags.Add(blogTag);
                }
            }

            _blogRepository.Add(blog);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}
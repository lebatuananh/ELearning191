using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Blog;
using PeaLearning.Common;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using PeaLearning.Domain.AggregateModels.TagAggregate;
using Shared.EF;
using Shared.Extensions;
using Shared.Utility;

namespace PeaLearning.Application.Write.BlogHandler
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;

        public UpdateBlogCommandHandler(IUnitOfWork uow, IBlogRepository blogRepository, ITagRepository tagRepository)
        {
            _uow = uow;
            _blogRepository = blogRepository;
            _tagRepository = tagRepository;
        }

        public async Task<Unit> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id);
            if (blog == null)
                throw new ArgumentNullException("Blog not found");
            blog.Update(request.Name, request.Thumbnail, request.Description,
                request.HomeFlag, request.HotFlag,
                request.SeoPageTitle, request.SeoAlias, request.SeoKeywords, request.SeoDescription, request.Status,
                request.Tags, request.Content);
            if (!string.IsNullOrEmpty(request.Tags))
            {
                blog.BlogTags.Clear();
                string[] tags = request.Tags.Split(',');
                var listTag = StringEqualityComparer.RemoveDuplicatesIterative(tags.ToList());
                foreach (string t in listTag)
                {
                    var text = t.Trim();
                    var tagId = text.ToSlug();
                    if (!await _tagRepository.ExistsAsync(x => x.Id == tagId))
                    {
                        Tag tag = new Tag(tagId, t, Constants.Tag.BlogTag);
                        _tagRepository.Add(tag);
                    }

                    BlogTag blogTag = new BlogTag
                    {
                        Id = new Guid(),
                        BlogId = blog.Id,
                        TagId = tagId
                    };
                    blog.BlogTags.Add(blogTag);
                }
            }

            _blogRepository.Update(blog);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}
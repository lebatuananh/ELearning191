using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PeaLearning.Application.Commands.Blog;
using PeaLearning.Domain.AggregateModels.BlogAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.BlogHandler
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogCommandHandler(IUnitOfWork uow, IBlogRepository blogRepository)
        {
            _uow = uow;
            _blogRepository = blogRepository;
        }

        public async Task<Unit> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetByIdAsync(request.Id);
            if (blog == null)
                throw new ArgumentNullException("Blog not found");
            _blogRepository.Delete(blog);
            await _uow.CommitAsync();
            return Unit.Value;
        }
    }
}
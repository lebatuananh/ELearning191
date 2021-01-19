using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;

namespace PeaLearning.Application.Read.BlogHandler
{
    public class GetBlogQueryHandler : IRequestHandler<GetBlogQuery, BlogDto>
    {
        private readonly IDbConnection _connection;

        public GetBlogQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<BlogDto> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            var blog = await _connection.QueryFirstOrDefaultAsync<BlogDto>("select * from blogs where id = @Id",
                new {request.Id});
            return blog;
        }
    }
}
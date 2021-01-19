using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;

namespace PeaLearning.Application.Read.BannerHandler
{
    public class GetBannerQueryHandler : IRequestHandler<GetBannerQuery, BannerDto>
    {
        private readonly IDbConnection _dbConnection;

        public GetBannerQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BannerDto> Handle(GetBannerQuery request, CancellationToken cancellationToken)
        {
            var banner =
                await _dbConnection.QueryFirstOrDefaultAsync<BannerDto>("select * from banners where id = @Id",
                    new {request.Id});
            return banner;
        }
    }
}
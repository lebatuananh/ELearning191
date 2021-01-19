using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;

namespace PeaLearning.Application.Read.UserHandler
{
    public class GetUserInformationQueryHandler : IRequestHandler<GetUserInformationQuery, UserDto>
    {
        private readonly IDbConnection _connection;

        public GetUserInformationQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<UserDto> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
        {
            var user =
                await _connection.QueryFirstOrDefaultAsync<UserDto>("select * from users where id = @Id",
                    new {request.Id});
            return user;
        }
    }
}
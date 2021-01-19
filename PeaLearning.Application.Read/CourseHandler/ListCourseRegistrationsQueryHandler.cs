using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using Shared.Dto;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class
        ListCourseRegistrationsQueryHandler : IRequestHandler<ListCourseRegistrationsQuery,
            QueryResult<UserRegistrationDto>>
    {
        private readonly IDbConnection _connection;

        public ListCourseRegistrationsQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<QueryResult<UserRegistrationDto>> Handle(ListCourseRegistrationsQuery request,
            CancellationToken cancellationToken)
        {
            var builder = new SqlBuilder();
            var tmplQueryItems = builder.AddTemplate(@"              
              select c.title,u.user_name,cr.created_date
              from course_registrations cr
              inner join courses c on c.id = cr.course_id
              inner join users u on cr.learner_id = u.id
                    /**where**/
                order by cr.created_date desc
                offset @Skip rows fetch next @Take row only");
            var tmplQueryCount = builder.AddTemplate(@"select count(cr.id) 
               from course_registrations cr
               inner join courses c on c.id = cr.course_id
               inner join users u on cr.learner_id = u.id
                /**where**/");
            if (!string.IsNullOrEmpty(request.Query))
            {
                builder.Where(@"c.id= @Query");
            }

            var queryStr = $@"
                {tmplQueryItems.RawSql};
                {tmplQueryCount.RawSql}";

            using var queryResults =
                await _connection.QueryMultipleAsync(queryStr, new {request.Skip, request.Take, request.Query});
            var items = queryResults.Read<UserRegistrationDto>();
            var count = queryResults.ReadFirst<int>();
            return new QueryResult<UserRegistrationDto>(count, items);
        }
    }
}
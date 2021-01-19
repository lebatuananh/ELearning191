using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Queries;
using PeaLearning.Common;

namespace PeaLearning.Api.Controllers.Admin
{
    [Authorize(Roles = Constants.Role.Administrator)]
    public class ContactController:AdminV1Controller
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> QueryCourses(string query, int skip, int take = 10)
        {
            var result = await _mediator.Send(new ListContactsQuery(skip, take, query));
            return Ok(result);
        }
    }
}
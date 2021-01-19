using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Api.Requests.Blog;
using PeaLearning.Application.Commands.Blog;
using PeaLearning.Application.Queries;
using PeaLearning.Common;

namespace PeaLearning.Api.Controllers.Admin
{
    [Authorize(Roles = Constants.Role.Administrator)]
    public class BlogController : AdminV1Controller
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogRequest addBlogRequest)
        {
            var result = await _mediator.Send(new AddBlogCommand(addBlogRequest.Name, addBlogRequest.Thumbnail,
                addBlogRequest.Description, addBlogRequest.HomeFlag, addBlogRequest.HotFlag,
                addBlogRequest.SeoPageTitle, addBlogRequest.SeoAlias, addBlogRequest.SeoKeywords,
                addBlogRequest.SeoDescription, addBlogRequest.Status, addBlogRequest.Tags, addBlogRequest.Content));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBlogRequest updateBlogRequest)
        {
            var result = await _mediator.Send(new UpdateBlogCommand(id, updateBlogRequest.Name,
                updateBlogRequest.Thumbnail,
                updateBlogRequest.Description, updateBlogRequest.HomeFlag, updateBlogRequest.HotFlag,
                updateBlogRequest.SeoPageTitle, updateBlogRequest.SeoAlias, updateBlogRequest.SeoKeywords,
                updateBlogRequest.SeoDescription, updateBlogRequest.Status, updateBlogRequest.Tags,
                updateBlogRequest.Content));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteBlogCommand(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> QueryCourses(string query, int skip, int take = 10)
        {
            var result = await _mediator.Send(new ListBlogsQuery(skip, take, query));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetBlogQuery(id));
            return Ok(result);
        }
    }
}
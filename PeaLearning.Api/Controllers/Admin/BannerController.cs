using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Api.Requests.Banner;
using PeaLearning.Application.Commands.Banner;
using PeaLearning.Application.Queries;
using PeaLearning.Common;

namespace PeaLearning.Api.Controllers.Admin
{
    [Authorize(Roles = Constants.Role.Administrator)]
    public class BannerController : AdminV1Controller
    {
        private readonly IMediator _mediator;

        public BannerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBannerRequest request)
        {
            var result = await _mediator.Send(new AddBannerCommand(request.Name, request.Thumbnail, request.Link,
                request.BannerPosition,request.BannerInPage));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBannerRequest request)
        {
            var result = await _mediator.Send(new UpdateBannerCommand(id, request.Name, request.Thumbnail, request.Link,
                request.BannerPosition,request.BannerInPage));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteBannerCommand(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> QueryBanner(string query, int skip, int take = 10)
        {
            var result = await _mediator.Send(new ListBannersQuery(skip, take, query));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetBannerQuery(id));
            return Ok(result);
        }
    }
}
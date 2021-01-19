using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Queries;

namespace PeaLearning.Website.Components
{
    public class BlogLatestViewComponent : BaseViewComponent
    {
        private readonly IMediator _mediator;
        public BlogLatestViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int limit = 3;
            var courses = await _mediator.Send(new ListLatestBlogQuery(limit));
            return View(courses);
        }
    }
}

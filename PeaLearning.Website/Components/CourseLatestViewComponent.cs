using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Queries;

namespace PeaLearning.Website.Components
{
    public class CourseLatestViewComponent : BaseViewComponent
    {
        private readonly IMediator _mediator;
        public CourseLatestViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int limit = 6;
            var courses = await _mediator.Send(new ListLatestCourseQuery(limit));
            return View(courses);
        }
    }
}

using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Queries;

namespace PeaLearning.Website.Components
{
    public class LessonLatestViewComponent : BaseViewComponent
    {
        private readonly IMediator _mediator;
        public LessonLatestViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int limit = 6;
            var lessons = await _mediator.Send(new ListLatestLessonQuery(limit));
            return View(lessons);
        }
    }
}

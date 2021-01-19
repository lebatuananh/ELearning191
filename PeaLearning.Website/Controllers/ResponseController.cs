using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using PeaLearning.Common.Utils;
using static PeaLearning.Common.Constants;

namespace PeaLearning.Website.Controllers
{
    [Authorize]
    public class ResponseController : Controller
    {
        private readonly IMediator _mediator;

        public ResponseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("response/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            AnalyzeResponseDto analyzeResponse = await _mediator.Send(new GetResponseQuery(id));
            ViewBag.TitlePage = SEO.AddTitle(analyzeResponse.Lesson.Title);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.CourseListDesc);
            return View(analyzeResponse);
        }
    }
}

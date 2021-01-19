using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Application.Queries;
using PeaLearning.Common.Utils;
using PeaLearning.Website.Requests;
using Shared.Infrastructure;
using static PeaLearning.Common.Constants;

namespace PeaLearning.Website.Controllers
{
    [Authorize]
    public class LessonController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserClaim _userClaim;
        public LessonController(IMediator mediator, IUserClaim userClaim)
        {
            _mediator = mediator;
            _userClaim = userClaim;
        }

        [Route("lesson/{name}-lid{lessonId}")]
        public async Task<IActionResult> Index(Guid lessonId)
        {
            var lesson = await _mediator.Send(new GetLessonQuery(lessonId));
            ViewBag.TitlePage = SEO.AddTitle(lesson.Title);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.CourseListDesc);
            return View(lesson);
        }

        [Route("course/{id}/lesson/{lessonId}/response")]
        [HttpPost]
        public async Task<IActionResult> ResponseLesson(Guid id, Guid lessonId, [FromBody] ResponseRequest request)
        {
            var responseId = await _mediator.Send(new SaveResponseQuery(id, lessonId, request.SubmittedDate, request.QuestionResponses, _userClaim.UserId, request.CompletedDuration));
            return Ok(new { ReturnUrl = $"/response/{responseId}" });
        }

    }
}


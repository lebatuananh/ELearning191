using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeaLearning.Api.Requests.Course;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Application.Queries;
using PeaLearning.Common;

namespace PeaLearning.Api.Controllers.Admin
{
    [Authorize(Roles = Constants.Role.Administrator)]
    public class CourseController : AdminV1Controller
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region course

        [HttpGet]
        public async Task<IActionResult> QueryCourses(string query, int skip, int take = 10)
        {
            var result = await _mediator.Send(new ListCoursesQuery(skip, take, query));
            return Ok(result);
        }

        [HttpGet("get-all-courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _mediator.Send(new ListCourseQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseRequest request)
        {
            var result =
                await _mediator.Send(new AddCourseCommand(request.Title, request.Description, request.Thumbnail,
                    request.IsPrice, request.Price));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetCourseQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCourseRequest request)
        {
            var result =
                await _mediator.Send(new UpdateCourseCommand(id, request.Title, request.Description,
                    request.Thumbnail, request.IsPrice, request.Price));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand(id));
            return Ok(result);
        }

        #endregion course

        #region exam

        [HttpGet("templateExam")]
        public async Task<IActionResult> DowloadTemplateAsync()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"SampleExam.xlsx");
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet("{id}/exam-management")]
        public async Task<IActionResult> QueryExamByCourses(Guid id, string query, int skip, int take = 10)
        {
            var result = await _mediator.Send(new ListExamByCoursesQuery(id, skip, take, query));
            return Ok(result);
        }

        [HttpPost("{id}/create-exam")]
        public async Task<IActionResult> AddExam(Guid id, AddExamRequest request)
        {
            var result = await _mediator.Send(new AddExamCommand(id, request.Title, request.Description,
                request.LessonType, request.Duration, request.IsActive));
            return Ok(result);
        }

        [HttpGet("exam/{id}")]
        public async Task<IActionResult> GetExam(Guid id)
        {
            var result = await _mediator.Send(new GetExamQuery(id));
            return Ok(result);
        }

        [HttpGet("exam/{id}/info")]
        public async Task<IActionResult> GetExamInfo(Guid id)
        {
            var result = await _mediator.Send(new GetExamInfoQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}/exam/{lessonId}")]
        public async Task<IActionResult> UpdateExam(Guid id, Guid lessonId, UpdateExamRequest request)
        {
            var result = await _mediator.Send(new UpdateExamCommand(id, lessonId, request.Title, request.Description,
                request.LessonType, request.Duration, request.IsActive));
            return Ok(result);
        }

        [HttpDelete("{id}/exam/{lessonId}")]
        public async Task<IActionResult> DeleteExam(Guid id, Guid lessonId)
        {
            var result = await _mediator.Send(new DeleteExamCommand(id, lessonId));
            return Ok(result);
        }

        [HttpPost("{id}/exam/{lessonId}/question")]
        public async Task<IActionResult> CreateQuestion(Guid id, Guid lessonId, [FromBody] AddQuestionRequest request)
        {
            var result = await _mediator.Send(new AddQuestionCommand(id, lessonId, request.Content, request.ParentId,
                request.Example, request.PictureUrl, request.AudioUrl, request.QuestionContent, request.Score));
            return Ok(result);
        }

        [HttpPut("{id}/exam/{lessonId}/question/{questionId}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, Guid lessonId, Guid questionId,
            [FromBody] AddQuestionRequest request)
        {
            var result = await _mediator.Send(new UpdateQuestionCommand(id, lessonId, questionId, request.Content,
                request.ParentId, request.Example, request.PictureUrl, request.AudioUrl, request.QuestionContent,
                request.Score));
            return Ok(result);
        }

        [HttpDelete("{id}/exam/{lessonId}/question/{questionId}")]
        public async Task<IActionResult> DeleteQuestion(Guid id, Guid lessonId, Guid questionId)
        {
            var result = await _mediator.Send(new DeleteQuestionCommand(id, lessonId, questionId));
            return Ok(result);
        }

        [HttpPost("{id}/importExam")]
        public async Task<IActionResult> ImportExam(Guid id, IFormFile file)
        {
            var result = await _mediator.Send(new ImportExamCommand(id, file));
            return Ok(result);
        }

        #endregion exam

        #region course_registration

        [HttpGet("query-course-registration")]
        public async Task<IActionResult> QueryCourseRegistration(string query, int skip, int take = 10)
        {
            var result = await _mediator.Send(new ListCourseRegistrationsQuery(skip, take, query));
            return Ok(result);
        }

        #endregion course_registration
    }
}
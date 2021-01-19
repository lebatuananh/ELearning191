using Dapper;
using MediatR;
using PeaLearning.Application.Models;
using PeaLearning.Application.Queries;
using PeaLearning.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PeaLearning.Application.Read.CourseHandler
{
    public class GetResponseQueryHandler : IRequestHandler<GetResponseQuery, AnalyzeResponseDto>
    {
        private readonly IDbConnection _connection;

        public GetResponseQueryHandler(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<AnalyzeResponseDto> Handle(GetResponseQuery request, CancellationToken cancellationToken)
        {
            var lessonDictionary = new Dictionary<Guid, LessonDto>();
            var response = await _connection.QueryFirstOrDefaultAsync<ResponseDto>(@"select * from responses where id = @Id", new { request.Id });
            var resultLesson = await _connection.QueryAsync<LessonDto, CourseDto, QuestionDto, LessonDto>(@"select les.*, crs.*, q.*, par.content as parent_content, par.example as parent_example
                    from lessons les
                    join courses crs
                    on crs.id = les.course_id
                    left join questions q
                    left join questions par
                    on q.parent_id = par.id
                    on les.id = q.lesson_id
                    where les.id = @Id
                    order by q.created_date", (lesson, course, question) =>
            {
                if (!lessonDictionary.TryGetValue(lesson.Id, out var lessonEntry))
                {
                    lessonEntry = lesson;
                    lessonEntry.Questions = new List<QuestionDto>();
                    lessonEntry.Course = course;
                    lessonDictionary.Add(lessonEntry.Id, lessonEntry);
                }
                if (question != null && lessonEntry.Questions.FirstOrDefault(i => i.Id == question.Id) == null && question.QuestionContent.QuestionType != QuestionType.Section)
                {
                    lessonEntry.Questions.Add(question);
                }
                return lessonEntry;
            }, new { Id = response.LessonId });
            var lesson = resultLesson.FirstOrDefault();
            int numberCorrectAnswer = 0;
            int totalPoint = 0;
            foreach (var item in lesson.Questions)
            {
                var res = response.QuestionResponses.FirstOrDefault(q => q.QuestionId == item.Id);
                item.QuestionResponse = res;
                switch (item.QuestionContent.QuestionType)
                {
                    case QuestionType.MultipleChoice:
                        MultipleChoiceResponse multipleChoiceResponse = (MultipleChoiceResponse)res;
                        if (multipleChoiceResponse.AnswerId == ((MultipleChoiceQuestion)item.QuestionContent).CorrectAnswerId)
                        {
                            numberCorrectAnswer++;
                            item.IsCorrect = true;
                            totalPoint += item.Score;
                        }
                        else
                        {
                            item.IsCorrect = false;
                        }
                        break;
                    case QuestionType.FillBlank:
                        FillBlankResponse fillBlankResponse = (FillBlankResponse)res;
                        if (fillBlankResponse.AnswerContent.ToLower() == ((FillBlankQuestion)item.QuestionContent).CorrectAnswerContent.ToLower())
                        {
                            numberCorrectAnswer++;
                            item.IsCorrect = true;
                            totalPoint += item.Score;
                        }
                        else
                        {
                            item.IsCorrect = false;
                        }
                        break;
                    case QuestionType.FillInPharagraph:
                        FillInPharagraphResponse fillInPharagraphResponse = (FillInPharagraphResponse)res;
                        var fillInPharagraphQuestion = ((FillInPharagraphQuestion)item.QuestionContent);
                        if (fillInPharagraphResponse.FillInPharagraphContentResponses.All(x =>
                        {
                            var option = fillInPharagraphQuestion.FillInPharagraphContents.FirstOrDefault(y => y.FillOptionId == x.FillOptionId);
                            if (option != null && option.CorrectAnswerId == x.AnswerId)
                                return true;
                            else
                                return false;
                        }))
                        {
                            numberCorrectAnswer++;
                            item.IsCorrect = true;
                            totalPoint += item.Score;
                        }
                        else
                        {
                            item.IsCorrect = false;
                        }
                        break;
                    case QuestionType.ArrangeWord:
                        ArrangeWordResponse arrangeWordResponse = (ArrangeWordResponse)res;
                        var arrangeWordQuestion = ((ArrangeWordQuestion)item.QuestionContent);
                        var setQuestion = new HashSet<Guid>(arrangeWordQuestion.ArrangeWordOptionQuestions.OrderBy(x => x.SortOrder).Select(x => x.Id));
                        var setResponse = new HashSet<Guid>(arrangeWordResponse.ArrangeWordOptionResponses.OrderBy(x => x.SortOrder).Select(x => x.Id));
                        if (setQuestion.SetEquals(setResponse))
                        {
                            numberCorrectAnswer++;
                            item.IsCorrect = true;
                            totalPoint += item.Score;
                        }
                        else
                        {
                            item.IsCorrect = false;
                        }
                        break;
                    case QuestionType.Matching:
                        MatchingResponse matchingResponse = (MatchingResponse)res;
                        if (matchingResponse.AnswerId == ((MatchingQuestion)item.QuestionContent).CorrectAnswerId)
                        {
                            numberCorrectAnswer++;
                            item.IsCorrect = true;
                            totalPoint += item.Score;
                        }
                        else
                        {
                            item.IsCorrect = false;
                        }
                        break;
                    case QuestionType.DragAndDrop:
                        DragAndDropResponse dragAndDropResponse = (DragAndDropResponse)res;
                        if (dragAndDropResponse.AnswerContent.ToLower() == ((DragAndDropQuestion)item.QuestionContent).CorrectAnswerContent.ToLower())
                        {
                            numberCorrectAnswer++;
                            item.IsCorrect = true;
                            totalPoint += item.Score;
                        }
                        else
                        {
                            item.IsCorrect = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            return new AnalyzeResponseDto()
            {
                NumOfCorrectAnswer = numberCorrectAnswer,
                Lesson = lesson,
                SubmittedDate = response.SubmittedDate,
                CompletedDuration = response.CompletedDuration,
                TotalPoint = totalPoint
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OfficeOpenXml;
using PeaLearning.Application.Commands.Course;
using PeaLearning.Common.Models;
using PeaLearning.Domain.AggregateModels.CourseAggregate;
using Shared.EF;

namespace PeaLearning.Application.Write.CourseHandler
{
    public class ImportExamCommandHandler : IRequestHandler<ImportExamCommand>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _uow;

        public ImportExamCommandHandler(ICourseRepository courseRepository, IUnitOfWork uow)
        {
            _courseRepository = courseRepository;
            _uow = uow;
        }

        public async Task<Unit> Handle(ImportExamCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new ArgumentNullException("Course not found");
            }
            var fileExtension = Path.GetExtension(request.File.FileName).ToUpper();
            if (fileExtension != ".XLSX")
            {
                throw new Exception("File type not supported, please upload Excel package");
            }

            if (request.File.Length <= 0)
            {
                throw new Exception("Invalid file");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(request.File.OpenReadStream()))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First();
                var rowEnd = worksheet.Dimension.End.Row;
                var lesson = new Lesson(Convert.ToString(worksheet.Cells[2, 1].Value), Convert.ToString(worksheet.Cells[2, 1].Value), (LessonType)Convert.ToInt32(worksheet.Cells[2, 4].Value), Convert.ToInt32(worksheet.Cells[2, 2].Value), Convert.ToBoolean(worksheet.Cells[2, 3].Value));
                course.AddLesson(lesson);
                _courseRepository.Update(course);
                var questions = new List<Question>();
                Guid? sectionId = null;
                ExcelRange currentSection = null;
                for (int row = 5; row <= rowEnd; row++)
                {
                    if (currentSection == null || worksheet.Cells[row, 1].Value != null)
                    {
                        currentSection = worksheet.Cells[row, 1];
                        if (currentSection.Merge)
                        {
                            var sectionContent = $"<p><span><strong>{worksheet.Cells[row, 2].GetValue<string>()}</strong></span><p>";
                            var listChoiceContent = worksheet.Cells[row, 7].GetValue<string>();
                            if (listChoiceContent != null)
                            {
                                sectionContent += @"<figure class=""table""><table><tbody>";
                                var listChoice = listChoiceContent.Split("|");
                                for (int i = 0; i < listChoice.Count(); i++)
                                {
                                    if (i % 3 == 0)
                                    {
                                        if (i == 0) sectionContent += "<tr>";
                                        else if (i == listChoice.Count() - 1)
                                            sectionContent += "</tr>";
                                        else sectionContent += "</tr><tr>";
                                    }
                                    sectionContent += $"<td>{listChoice[i]}</td>";
                                }
                                sectionContent += "</tbody></table></figure>";
                            }
                            var section = new Question(sectionContent, null, worksheet.Cells[row, 4].GetValue<string>(), worksheet.Cells[row, 3].GetValue<string>(), worksheet.Cells[row, 5].GetValue<string>(), new QuestionContent() { QuestionType = QuestionType.Section }, 0);
                            course.AddQuestion(lesson.Id, section);
                            _courseRepository.Update(course);
                            await _uow.CommitAsync();
                            sectionId = section.Id;
                        }
                    }
                    var questionType = (QuestionType)Convert.ToInt32(worksheet.Cells[row, 10].Value);
                    Question question;
                    switch (questionType)
                    {
                        case QuestionType.FillBlank:
                            question = new Question($"<p>{worksheet.Cells[row, 6].GetValue<string>()}</p>", sectionId, string.Empty, string.Empty, string.Empty, new FillBlankQuestion() { QuestionType = QuestionType.FillBlank, CorrectAnswerContent = worksheet.Cells[row, 8].GetValue<string>() }, Convert.ToInt32(worksheet.Cells[row, 11].Value));
                            course.AddQuestion(lesson.Id, question);
                            break;

                        case QuestionType.FillInPharagraph:
                            FillInPharagraphQuestion contentPharagraph = new FillInPharagraphQuestion();
                            contentPharagraph.QuestionType = QuestionType.FillInPharagraph;
                            var fillInPharagraphContents = new List<FillInPharagraphContent>();
                            var questionContent = worksheet.Cells[row, 6].GetValue<string>().Replace("\n", "");
                            string[] source = questionContent.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            var fillChoices = new List<ChoiceAnswer>();
                            var listFillChoiceContent = worksheet.Cells[row, 7].GetValue<string>().Replace("\n", "");
                            foreach (var item in listFillChoiceContent.Split("|"))
                            {
                                fillChoices.Add(new ChoiceAnswer()
                                {
                                    Id = Guid.NewGuid(),
                                    Content = item
                                });
                            }
                            var listCorrectAnswer = worksheet.Cells[row, 8].GetValue<string>().Replace("\n", "").Split("|");
                            for (int i = 0; i < source.Count(x => x.Contains("{{keyword")); i++)
                            {
                                fillInPharagraphContents.Add(new FillInPharagraphContent()
                                {
                                    FillOptionId = Guid.NewGuid(),
                                    FillOption = "{{keyword" + i + "}}",
                                    Choices = fillChoices.OrderBy(c => Guid.NewGuid()).ToList(),
                                    CorrectAnswerId = fillChoices.FirstOrDefault(c => c.Content.ToLower() == listCorrectAnswer[i].ToLower()).Id
                                });
                            }
                            contentPharagraph.FillInPharagraphContents = fillInPharagraphContents;
                            question = new Question($"<p>{questionContent}</p>", null, worksheet.Cells[row, 4].GetValue<string>(), worksheet.Cells[row, 3].GetValue<string>(), worksheet.Cells[row, 5].GetValue<string>(), contentPharagraph, Convert.ToInt32(worksheet.Cells[row, 11].Value));
                            course.AddQuestion(lesson.Id, question);
                            break;

                        case QuestionType.ArrangeWord:
                            ArrangeWordQuestion arrangeWordQuestion = new ArrangeWordQuestion();
                            arrangeWordQuestion.QuestionType = QuestionType.ArrangeWord;
                            var arrangeWordOptions = new List<ArrangeWordOptionQuestion>();
                            var listOrders = worksheet.Cells[row, 6].GetValue<string>().Split("|");
                            var listOrdereds = worksheet.Cells[row, 8].GetValue<string>().Split("|").ToList();
                            for (int i = 0; i < listOrders.Count(); i++)
                            {
                                arrangeWordOptions.Add(new ArrangeWordOptionQuestion()
                                {
                                    Id = Guid.NewGuid(),
                                    Word = listOrders[i],
                                    SortOrder = listOrdereds.FindIndex(x => x == listOrders[i])
                                });
                            }
                            arrangeWordQuestion.ArrangeWordOptionQuestions = arrangeWordOptions;
                            question = new Question($"{worksheet.Cells[row, 6].GetValue<string>().Replace("\n", "")}", sectionId, string.Empty, string.Empty, string.Empty, arrangeWordQuestion, Convert.ToInt32(worksheet.Cells[row, 11].Value));
                            course.AddQuestion(lesson.Id, question);
                            break;

                        case QuestionType.MultipleChoice:
                            MultipleChoiceQuestion content = new MultipleChoiceQuestion();
                            content.QuestionType = QuestionType.MultipleChoice;
                            var choices = new List<ChoiceAnswer>();
                            var listChoiceContent = worksheet.Cells[row, 7].GetValue<string>();
                            foreach (var item in listChoiceContent.Split("|"))
                            {
                                choices.Add(new ChoiceAnswer()
                                {
                                    Id = Guid.NewGuid(),
                                    Content = item
                                });
                            }
                            content.Choices = choices;
                            content.CorrectAnswerId = choices.FirstOrDefault(c => c.Content.ToLower() == worksheet.Cells[row, 8].GetValue<string>().ToLower()).Id;
                            question = new Question($"<p>{worksheet.Cells[row, 6].GetValue<string>().Replace("\n", "")}</p>", null, worksheet.Cells[row, 4].GetValue<string>(), worksheet.Cells[row, 3].GetValue<string>(), worksheet.Cells[row, 5].GetValue<string>(), content, Convert.ToInt32(worksheet.Cells[row, 11].Value));
                            course.AddQuestion(lesson.Id, question);
                            break;

                        default:
                            break;
                    }
                    _courseRepository.Update(course);
                    await _uow.CommitAsync();
                }
                return Unit.Value;
            }
        }
    }
}
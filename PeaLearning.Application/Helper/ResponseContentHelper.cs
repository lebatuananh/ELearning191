using Microsoft.EntityFrameworkCore.Internal;
using PeaLearning.Application.Models;
using PeaLearning.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PeaLearning.Application.Helper
{
    public static class ResponseContentHelper
    {
        public static string ConvertResponseContent(this QuestionDto model)
        {
            if (model.QuestionContent.QuestionType == QuestionType.FillBlank)
            {
                var response = (FillBlankResponse)model.QuestionResponse;
                FillBlankQuestion fillBlankQuestion = JsonSerializer.Deserialize<FillBlankQuestion>(model.QuestionContentRaw);
                string pattern = "{{keyword(\\d?)}}";
                string input = "<input type=\"text\" class=\"user-result form-control form-control-sm d-inline w-auto\" name=\"" + model.Id + "\" value=\"" + response.AnswerContent + "\" >";
                model.Content = string.Concat("<div class=\"d-flex align-items-center\">", input);
                if (model.IsCorrect)
                {
                    model.Content = string.Concat(model.Content, "<i class=\"fa-check fas ml-2 text-primary\"></i>");
                }
                else
                {
                    model.Content = string.Concat(model.Content, "<i class=\"fa-times fas ml-2 text-danger\"></i>");
                }
                model.Content = string.Concat(model.Content, "</div>");
                if (model.Content.IndexOf("{{keyword") > -1)
                    model.Content = Regex.Replace(model.Content, pattern, input);
                if (!model.IsCorrect)
                {
                    model.Content = string.Concat(model.Content, "<p class=\"text-muted mt-2\">Đáp án đúng: <strong>" + fillBlankQuestion.CorrectAnswerContent + "</strong></p>");
                }
            }
            else if (model.QuestionContent.QuestionType == QuestionType.FillInPharagraph)
            {
                var response = (FillInPharagraphResponse)model.QuestionResponse;
                FillInPharagraphQuestion fillInPharagraphQuestion = JsonSerializer.Deserialize<FillInPharagraphQuestion>(model.QuestionContentRaw);
                if (fillInPharagraphQuestion != null)
                {
                    int i = 0;
                    foreach (var item in fillInPharagraphQuestion.FillInPharagraphContents)
                    {
                        var responseContent = response.FillInPharagraphContentResponses.FirstOrDefault(x => x.FillOptionId == item.FillOptionId);
                        i++;
                        StringBuilder stringBuilder = new StringBuilder();
                        if (responseContent.AnswerId == item.CorrectAnswerId)
                        {
                            stringBuilder.AppendFormat("<select id=\"{0}\" data-id=\"{1}\" class=\"form-control form-control-sm d-inline w-auto \" >", string.Concat(model.Id, "-", i), item.FillOptionId);
                        }
                        else
                            stringBuilder.AppendFormat("<select id=\"{0}\" data-id=\"{1}\" class=\"form-control form-control-sm d-inline w-auto border-danger\" >", string.Concat(model.Id, "-", i), item.FillOptionId);
                        foreach (var subItem in item.Choices)
                        {
                            if (responseContent.AnswerId == subItem.Id)
                            {
                                stringBuilder.AppendFormat("<option value=\"{0}\" selected>{1}</option>", subItem.Id, subItem.Content);
                            }
                            else
                                stringBuilder.AppendFormat("<option value=\"{0}\">{1}</option>", subItem.Id, subItem.Content);
                        }
                        stringBuilder.Append("</select>");
                        string keywordReplace = item.FillOption;
                        string keywordNew = stringBuilder.ToString();
                        model.Content = model.Content.Replace(keywordReplace, keywordNew);
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<div class=\"mt-2\"><h5 class=\"card-title\">Đáp án :</h5><ul class=\"list-unstyled feature-list text-muted\">");
                    int j = 0;
                    foreach (var item in fillInPharagraphQuestion.FillInPharagraphContents)
                    {
                        j++;
                        sb.AppendFormat("<li><i data-feather=\"arrow-right\" class=\"fea icon-sm mr-2\"></i>" + j.ToString() + " " + item.Choices.FirstOrDefault(x => x.Id == item.CorrectAnswerId).Content + "</li>");
                    }
                    sb.AppendFormat("</ul></div>");
                    model.Content = string.Concat(model.Content, sb.ToString());
                }
            }
            else if (model.QuestionContent.QuestionType == QuestionType.ArrangeWord)
            {
                var response = (ArrangeWordResponse)model.QuestionResponse;
                ArrangeWordQuestion arrangeWordQuestion = JsonSerializer.Deserialize<ArrangeWordQuestion>(model.QuestionContentRaw);
                int i = 0;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class=\"align-items-center col-md-8 d-flex\">");
                foreach (var item in arrangeWordQuestion.ArrangeWordOptionQuestions)
                {
                    i++;
                    var responseContent = response.ArrangeWordOptionResponses.FirstOrDefault(x => x.SortOrder == i);
                    stringBuilder.AppendFormat("<select id=\"{0}\" class=\"form-control form-control-sm mr-3\" >", string.Concat(model.Id, "-", i));
                    foreach (var subItem in arrangeWordQuestion.ArrangeWordOptionQuestions)
                    {
                        if (responseContent.Id == subItem.Id)
                        {
                            stringBuilder.AppendFormat("<option value=\"{0}\" selected>{1}</option>", subItem.Id, subItem.Word);
                        }
                        else
                            stringBuilder.AppendFormat("<option value=\"{0}\">{1}</option>", subItem.Id, subItem.Word);
                    }
                    stringBuilder.Append("</select>");
                }
                if (model.IsCorrect)
                {
                    stringBuilder.AppendFormat("<i class=\"fa-check fas ml-2 text-primary\"></i>");
                }
                else
                {
                    stringBuilder.AppendFormat("<i class=\"fa-times fas ml-2 text-danger\"></i>");
                }
                stringBuilder.Append("</div>");
                model.Content = string.Concat(stringBuilder.ToString());
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<div class=\"mt-3\"><h5 class=\"card-title\">Đáp án :</h5>");
                sb.AppendFormat("<p class=\"text-muted\">" + string.Join(" ", arrangeWordQuestion.ArrangeWordOptionQuestions.OrderBy(x => x.SortOrder).Select(x => x.Word)) + "</p>");
                sb.AppendFormat("</div>");
                model.Content = string.Concat(model.Content, sb.ToString());
            }
            else if (model.QuestionContent.QuestionType == QuestionType.MultipleChoice)
            {
                var response = (MultipleChoiceResponse)model.QuestionResponse;
                StringBuilder stringBuilder = new StringBuilder();
                MultipleChoiceQuestion multipleChoiceQuestion = JsonSerializer.Deserialize<MultipleChoiceQuestion>(model.QuestionContentRaw);
                if (multipleChoiceQuestion != null)
                {
                    int i = 0;
                    foreach (var item in multipleChoiceQuestion.Choices)
                    {
                        i++;
                        if (response.AnswerId == item.Id)
                        {
                            if (model.IsCorrect)
                            {
                                stringBuilder.AppendFormat("<div class=\"custom-control custom-radio align-items-center custom-control-inline mb-2\"><div class=\"form-group mb-0\"><input type=\"radio\" id=\"{0}\" name=\"customRadio\" value=\"{3}\" data-id=\"{2}\" data-lessonid=\"{3}\" class=\"custom-control-input\" checked><label class=\"custom-control-label\" for=\"{0}\">{1}</label></div><i class=\"fa-check fas ml-2 text-primary\"></i></div> <br/>", string.Concat(model.Id, "-", i), item.Content, model.Id, item.Id, model.LessonId);
                            }
                            else
                            {
                                stringBuilder.AppendFormat("<div class=\"custom-control custom-radio align-items-center custom-control-inline mb-2\"><div class=\"form-group mb-0\"><input type=\"radio\" id=\"{0}\" name=\"customRadio\" value=\"{3}\" data-id=\"{2}\" data-lessonid=\"{3}\" class=\"custom-control-input\" checked><label class=\"custom-control-label\" for=\"{0}\">{1}</label></div><i class=\"fa-times fas ml-2 text-danger\"></i></div> <br/>", string.Concat(model.Id, "-", i), item.Content, model.Id, item.Id, model.LessonId);
                            }
                        }
                        else
                        {
                            if (item.Id == multipleChoiceQuestion.CorrectAnswerId)
                            {
                                stringBuilder.AppendFormat("<div class=\"custom-control custom-radio align-items-center custom-control-inline mb-2\"><div class=\"form-group mb-0\"><input type=\"radio\" id=\"{0}\" name=\"customRadio\" value=\"{3}\" data-id=\"{2}\" data-lessonid=\"{3}\" class=\"custom-control-input\"><label class=\"custom-control-label\" for=\"{0}\">{1}</label></div><i class=\"fa-check fas ml-2 text-primary\"></i></div> <br/>", string.Concat(model.Id, "-", i), item.Content, model.Id, item.Id, model.LessonId);
                            }
                            else
                            {
                                stringBuilder.AppendFormat("<div class=\"custom-control custom-radio align-items-center custom-control-inline mb-2\"><div class=\"form-group mb-0\"><input type=\"radio\" id=\"{0}\" name=\"customRadio\" value=\"{3}\" data-id=\"{2}\" data-lessonid=\"{3}\" class=\"custom-control-input\"><label class=\"custom-control-label\" for=\"{0}\">{1}</label></div></div> <br/>", string.Concat(model.Id, "-", i), item.Content, model.Id, item.Id, model.LessonId);
                            }
                        }
                    }
                }
                model.Content = string.Concat(model.Content, stringBuilder.ToString());
            }
            else if (model.QuestionContent.QuestionType == QuestionType.Record)
            {

            }
            else if (model.QuestionContent.QuestionType == QuestionType.DragAndDrop)
            {
                DragAndDropQuestion dragAndDropQuestion = JsonSerializer.Deserialize<DragAndDropQuestion>(model.QuestionContentRaw);
            }
            else if (model.QuestionContent.QuestionType == QuestionType.Matching)
            {
                MatchingQuestion matchingQuestion = JsonSerializer.Deserialize<MatchingQuestion>(model.QuestionContentRaw);
                var response = (MatchingResponse)model.QuestionResponse;
                //int keywordCount = Regex.Matches(model.Content, pattern).Count;
                if (matchingQuestion != null)
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    if (model.IsCorrect)
                    {
                        stringBuilder.AppendFormat("<select id=\"{0}\" data-id=\"{1}\" data-lessonid=\"{2}\" class=\"form-control form-control-sm d-inline w-auto\">", string.Concat(model.Id), model.Id, model.LessonId);
                    }
                    else
                    {
                        stringBuilder.AppendFormat("<select id=\"{0}\" data-id=\"{1}\" data-lessonid=\"{2}\" class=\"form-control form-control-sm d-inline w-auto border-danger\">", string.Concat(model.Id), model.Id, model.LessonId);
                    }
                    foreach (var item in matchingQuestion.Choices)
                    {
                        if (item.Id == response.AnswerId)
                        {
                            stringBuilder.AppendFormat("<option value=\"{0}\" selected>{1}</option>", item.Id, item.Content);
                        }
                        else
                        {
                            stringBuilder.AppendFormat("<option value=\"{0}\">{1}</option>", item.Id, item.Content);
                        }
                    }
                    stringBuilder.Append("</select>");
                    model.Content = string.Concat(model.Content, stringBuilder.ToString());
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("<div class=\"mt-3\"><h5 class=\"card-title\">Đáp án :</h5>");
                    sb.AppendFormat("<p class=\"text-muted\">" + matchingQuestion.Choices.FirstOrDefault(x => x.Id == matchingQuestion.CorrectAnswerId).Content + "</p>");
                    sb.AppendFormat("</div>");
                    model.Content = string.Concat(model.Content, sb.ToString());
                }
            }
            return model.Content;
        }
    }
}

using PeaLearning.Application.Models;
using PeaLearning.Common.Models;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PeaLearning.Application.Helper
{
    public static class ContentHelper
    {
        public static string ConvertContent(this QuestionDto model)
        {
            if (model.QuestionContent.QuestionType == QuestionType.FillBlank)
            {
                string pattern = "{{keyword(\\d?)}}";
                string input = "<input type=\"text\" class=\"user-result form-control form-control-sm d-inline w-auto\" name=\"" + model.Id + "\" id=\"hdd-" + model.Id + "$1\" >";
                if (model.Content.IndexOf("{{keyword") > -1)
                    model.Content = Regex.Replace(model.Content, pattern, input);
                else
                    model.Content = string.Concat(model.Content, input);
                model.Content = string.Concat(model.Content, "<input type=\"hidden\" id=\"hddQuestionId\" value=\"" + model.Id + "\" />");
            }
            else if (model.QuestionContent.QuestionType == QuestionType.FillInPharagraph)
            {
                FillInPharagraphQuestion fillInPharagraphQuestion = JsonSerializer.Deserialize<FillInPharagraphQuestion>(model.QuestionContentRaw);
                if (fillInPharagraphQuestion != null)
                {
                    int i = 0;
                    foreach (var item in fillInPharagraphQuestion.FillInPharagraphContents)
                    {
                        i++;
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendFormat("<select id=\"{0}\" data-id=\"{1}\" class=\"form-control form-control-sm d-inline w-auto\">", string.Concat(model.Id, "-", i), item.FillOptionId);
                        foreach (var subItem in item.Choices)
                        {
                            stringBuilder.AppendFormat("<option value=\"{0}\">{1}</option>", subItem.Id, subItem.Content);
                        }
                        stringBuilder.Append("</select>");
                        string keywordReplace = item.FillOption;
                        string keywordNew = stringBuilder.ToString();
                        model.Content = model.Content.Replace(keywordReplace, keywordNew);
                    }
                }
                model.Content = string.Concat(model.Content, "<input type=\"hidden\" id=\"hddQuestionId\" value=\"" + model.Id + "\" />");
            }
            else if (model.QuestionContent.QuestionType == QuestionType.ArrangeWord)
            {
                ArrangeWordQuestion arrangeWordQuestion = JsonSerializer.Deserialize<ArrangeWordQuestion>(model.QuestionContentRaw);
                int i = 0;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class=\"d-flex col-md-8\">");
                foreach (var item in arrangeWordQuestion.ArrangeWordOptionQuestions)
                {
                    i++;
                    stringBuilder.AppendFormat("<select id=\"{0}\" class=\"form-control form-control-sm mr-3\">", string.Concat(model.Id, "-", i));
                    foreach (var subItem in arrangeWordQuestion.ArrangeWordOptionQuestions)
                    {
                        stringBuilder.AppendFormat("<option value=\"{0}\">{1}</option>", subItem.Id, subItem.Word);
                    }
                    stringBuilder.Append("</select>");
                }
                stringBuilder.Append("</div>");
                model.Content = string.Concat(stringBuilder.ToString(), "<input type=\"hidden\" id=\"hddQuestionId\" value=\"" + model.Id + "\" />");
            }
            else if (model.QuestionContent.QuestionType == QuestionType.MultipleChoice)
            {
                StringBuilder stringBuilder = new StringBuilder();
                MultipleChoiceQuestion multipleChoiceQuestion = JsonSerializer.Deserialize<MultipleChoiceQuestion>(model.QuestionContentRaw);
                if (multipleChoiceQuestion != null)
                {
                    int i = 0;
                    foreach (var item in multipleChoiceQuestion.Choices)
                    {
                        i++;
                        stringBuilder.AppendFormat("<div class=\"custom-control custom-radio custom-control-inline mb-2\"><div class=\"form-group mb-0\"><input type=\"radio\" id=\"{0}\" name=\"customRadio\" value=\"{3}\" data-id=\"{2}\" data-lessonid=\"{3}\" class=\"custom-control-input\"><label class=\"custom-control-label\" for=\"{0}\">{1}</label></div></div> <br/>", string.Concat(model.Id, "-", i), item.Content, model.Id, item.Id, model.LessonId);
                    }
                }
                model.Content = string.Concat(model.Content, stringBuilder.ToString(), "<input type=\"hidden\" id=\"hddQuestionId\" value=\"" + model.Id + "\" />");
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
                //int keywordCount = Regex.Matches(model.Content, pattern).Count;
                if (matchingQuestion != null)
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendFormat("<select id=\"{0}\" data-id=\"{1}\" data-lessonid=\"{2}\">", string.Concat(model.Id), model.Id, model.LessonId);
                    foreach (var item in matchingQuestion.Choices)
                    {
                        stringBuilder.AppendFormat("<option value=\"{0}\">{1}</option>", item.Id, item.Content);
                    }
                    stringBuilder.Append("</select>");
                    model.Content = string.Concat(model.Content, stringBuilder.ToString());
                }
                model.Content = string.Concat(model.Content, "<input type=\"hidden\" id=\"hddQuestionId\" value=\"" + model.Id + "\" />");
            }
            return model.Content;
        }
    }
}

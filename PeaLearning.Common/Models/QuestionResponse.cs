using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PeaLearning.Common.Models
{
    [JsonConverter(typeof(QuestionResponseConverter))]
    public class QuestionResponse
    {
        public Guid QuestionId { get; set; }
        public QuestionType QuestionType { get; set; }
    }

    public class QuestionResponseConverter : JsonConverter<QuestionResponse>
    {
        public override QuestionResponse ReadJson(JsonReader reader, Type objectType,
            [AllowNull] QuestionResponse existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var questionType = (QuestionType)(jo["QuestionType"] ?? jo["questionType"]).Value<int>();
            Type targetType = questionType switch
            {
                QuestionType.MultipleChoice => typeof(MultipleChoiceResponse),
                QuestionType.FillBlank => typeof(FillBlankResponse),
                QuestionType.FillInPharagraph => typeof(FillInPharagraphResponse),
                QuestionType.Section => typeof(QuestionResponse),
                QuestionType.ArrangeWord => typeof(ArrangeWordResponse),
                QuestionType.Matching => typeof(MatchingResponse),
                QuestionType.DragAndDrop => typeof(DragAndDropResponse),
                QuestionType.Record => typeof(RecordQuestionResponse),
                _ => throw new Exception()
            };
            var target = (QuestionResponse)Activator.CreateInstance(targetType);
            serializer.Populate(jo.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] QuestionResponse value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}
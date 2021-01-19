using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace PeaLearning.Common.Models
{
    [JsonConverter(typeof(QuestionContentConverter))]
    public class QuestionContent
    {
        public QuestionType QuestionType { get; set; }
    }

    public class QuestionContentConverter : JsonConverter<QuestionContent>
    {
        public override QuestionContent ReadJson(JsonReader reader, Type objectType,
            [AllowNull] QuestionContent existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var questionType = (QuestionType)(jo["QuestionType"] ?? jo["questionType"]).Value<int>();
            Type targetType = questionType switch
            {
                QuestionType.MultipleChoice => typeof(MultipleChoiceQuestion),
                QuestionType.FillBlank => typeof(FillBlankQuestion),
                QuestionType.FillInPharagraph => typeof(FillInPharagraphQuestion),
                QuestionType.Section => typeof(QuestionContent),
                QuestionType.ArrangeWord => typeof(ArrangeWordQuestion),
                QuestionType.Matching => typeof(MatchingQuestion),
                QuestionType.DragAndDrop => typeof(DragAndDropQuestion),
                QuestionType.Record => typeof(RecordQuestion),
                _ => throw new Exception()
            };
            var target = (QuestionContent)Activator.CreateInstance(targetType);
            serializer.Populate(jo.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] QuestionContent value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}
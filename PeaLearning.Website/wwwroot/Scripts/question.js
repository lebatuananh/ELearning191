/*
DES: Scripts for homepage
*/
const QuestionType = {
    MultipleChoice: 0,
    FillBlank: 1,
    FillInPharagraph: 2,
    ArrangeWord: 3,
    Record: 4,
    DragAndDrop: 5,
    Matching: 6
}

const LessonType = {
    Audit: 1,
    Practise: 0
}

var startDate = new Date()

var question = {
    nextQuestion: function (index) {
        var questions = $('.page-item .page-link')
        for (i = 0; i < questions.length; i++) {
            questions.parent().removeClass('active')
        }
        var me = $(index),
            stt = me.data("stt"),
            question = stt + 1;
        var nextActiveQuestion = $('.page-item .page-link[data-id="' + question + '"]');
        nextActiveQuestion.parent().addClass('active')
        $('#loadQuestion .col-lg-12').hide();
        $('#stt-' + question).show();
    },
    chooseQuestion: function (index) {
        var questions = $('.page-item .page-link')
        for (i = 0; i < questions.length; i++) {
            questions.parent().removeClass('active')
        }
        var $me = $(index),
            question = $me.data("id")
        $me.parent().addClass('active')
        $('#loadQuestion .col-lg-12').hide();
        $('#stt-' + question).show();
    },
    confirm: function () {
        $("#confirmModal").modal('show');
    },
    submit: function () {
        $("#confirmModal").modal('hide');
        common.loading();
        var questions = $('.media-body.answer')
        let responses = {
            submittedDate: new Date(),
            questionResponses: [],
            completedDuration: parseInt((new Date() - startDate) / 1000)
        };
        for (i = 0; i < questions.length; i++) {
            const questionType = $(questions[i]).data("type");
            switch (questionType) {
                case QuestionType.FillBlank:
                    responses.questionResponses.push({
                        questionId: $(questions[i]).children("#hddQuestionId").val(),
                        questionType: questionType,
                        answerContent: $(`#hdd-${$(questions[i]).children("#hddQuestionId").val()}`).val()
                    })
                    break;
                case QuestionType.FillInPharagraph:
                    const selects = $(questions[i]).find('select');
                    let fillInPharagraphContentResponses = [];
                    for (let j = 0; j < selects.length; j++) {
                        fillInPharagraphContentResponses.push({
                            fillOptionId: $(selects[j]).data("id"),
                            answerId: $(selects[j]).val()
                        })
                    }
                    responses.questionResponses.push({
                        questionId: $(questions[i]).children("#hddQuestionId").val(),
                        questionType: questionType,
                        fillInPharagraphContentResponses: fillInPharagraphContentResponses
                    })
                    break;
                case QuestionType.ArrangeWord:
                    let selectArrangeWords = $(questions[i]).find('select');
                    let arrangeWordOptionResponses = [];
                    for (let j = 0; j < selectArrangeWords.length; j++) {
                        arrangeWordOptionResponses.push({
                            id: $(selectArrangeWords[j]).val(),
                            sortOrder: j + 1
                        })
                    }
                    responses.questionResponses.push({
                        questionId: $(questions[i]).children("#hddQuestionId").val(),
                        questionType: questionType,
                        arrangeWordOptionResponses: arrangeWordOptionResponses
                    })
                    break;
                case QuestionType.MultipleChoice:
                    responses.questionResponses.push({
                        questionId: $(questions[i]).children("#hddQuestionId").val(),
                        questionType: questionType,
                        answerId: $(questions[i]).find('input[type="radio"]:checked').val()
                    })
                    break;
                case QuestionType.Matching:
                    responses.questionResponses.push({
                        questionId: $(questions[i]).children("#hddQuestionId").val(),
                        questionType: questionType,
                        answerId: $(questions[i]).find('select').val()
                    })
                    break;
                default:
                    break;
            }
        };
        $.ajax({
            type: "POST",
            url: `/course/${$('#hddCourseId').val()}/lesson/${$('#hddLessonId').val()}/response`,
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(responses),
            success: function (response) {
                common.loaded();
                location.href = response.returnUrl
            },
            error: function () {
            }
        })
    }
}
$('#exam-count-down').countdown($('#hddDuration').val(), function (event) {
    var $this = $(this).html(event.strftime(''
        + '<strong>%M:%S</strong>'
    ));
}).on('finish.countdown', function (event) {
    const lessonType = $('#hddLessonType').val()
    if (lessonType == LessonType.Audit) {
        question.submit()
    }
    else {
        $(this).html('<strong>Đã hết thời gian làm bài!<strong>')
    }
});
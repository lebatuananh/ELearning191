/*
DES: Scripts for homepage
*/

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
    }
}
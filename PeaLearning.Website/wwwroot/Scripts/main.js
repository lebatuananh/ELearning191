/*
DES: Scripts main
*/
var main = {
    init: function () {
        main.initForm();
    },
    initForm: function () {
        $('button[type=reset]').click(function () {
            $('.field-validation-error').empty();
        });
        $('button[type=submit]').click(function () {
            var form = $(this).parents('form:first');
            $(form).off("submit");
            if (form.valid()) {
                form.submit(function (e) {
                    var data = form.serialize();
                    if (this.beenSubmitted == data)
                        return false;
                    else {
                        common.loading();
                        this.beenSubmitted = data;
                        $.post(form.attr('action'), data, function (response) {
                            if (response.error) {
                                notifys.createMessageError(response.title, form);
                            }
                            else {
                                switch (response.nextAction) {
                                    case 1:
                                        notifys.reloadPage(response.title);
                                        break;
                                    case 2:
                                        location.href = response.title || '/';
                                        break;
                                    default:
                                        notifys.createMessageError(response.title, form);
                                        break;
                                }
                            }
                            common.loaded();
                        });
                        return false;
                    }
                });
            } else {
                $('#lblMessage').empty();
                return false;
            }
        });
    },
    initFormPopup: function () {
        var form = $('#popup form:first');
        if (form.valid()) {
            form.submit(function (e) {
                var data = form.serialize();
                if (this.beenSubmitted == data)
                    return false;
                else {
                    common.loading();
                    this.beenSubmitted = data;
                    $.post(form.attr('action'), data, function (response) {
                        if (response.error) {
                            notifys.createMessageErrorPopup(response.title, form);
                        }
                        else {
                            switch (response.nextAction) {
                                case 1:
                                    notifys.reloadPage(response.title);
                                    break;
                                case 2:
                                    location.href = response.title;
                                    break;
                                default:
                                    notifys.createMessageErrorPopup(response.title, form);
                                    break;
                            }
                        }
                        common.loaded();
                    });
                    return false;
                }
            });
        }
    },

    fixedMenuScroll: function () {
        if ($(document).height() - $(window).height() > 300) {
            $(window).scroll(function () {
                if ($(window).scrollTop() >= 80) {
                    $("#header-des").addClass("affix");
                    $('.go-top').fadeIn(200);
                } else if ($("#header-des").hasClass('affix')) {
                    $("#header-des").removeClass("affix");
                    $('.go-top').fadeOut(200);
                }
            });
        }
    },
    backToTop: function () {
        //$(window).scroll(function () {
        //    if ($(this).scrollTop() > 200) {
        //        $('.go-top').fadeIn(200);
        //    } else {
        //        $('.go-top').fadeOut(200);
        //    }
        //});
        $('.go-top').click(function (event) {
            $('html, body').animate({ scrollTop: 0 }, 300);
        })
    }
};


var common = {
    loading: function () {
        $('#loading').html('<div class="bg-popup"> <img src="/Content/images/loading300.gif" class="loading"/></div>');
        common.noScroll();
    },
    loaded: function () {
        $('#loading').empty();
        common.autoScroll();
    },
    noScroll: function () {
        var width = $('body').width();
        $('body').css('overflow', 'hidden');
        var scrollWidth = $('body').width() - width;
        $('body').css('margin-right', scrollWidth + 'px');
    },
    autoScroll: function () {
        $('body').removeAttr('style');
    },
    closePopup: function () {
        $('#popup').empty();
        common.autoScroll();
    }
};

var notifys = {
    createMessageError: function (strMessage, form) {
        var message = $(form).find("#lblMessage");
        message.html("<div class=\"field-validation-error red-clr\">" + strMessage + "</div>");
    },
    createMessageErrorPopup: function (strMessage, form) {
        var message = $(form).find("#lblMessage");
        message.html("<div class=\"field-validation-error red-clr\">" + strMessage + "</div>");
    },
    reloadPage: function (strMessage) {
        alert(strMessage);
        location.reload(true);
    }
};
/*
DES: Scripts initial
*/

var initpage = {
    init: function () {
        $(document).ready(function () {
            if (typeof main != "undefined") {
                main.init();
            }
            if (typeof home != "undefined") {
                home.init();
            }
            if (typeof question != "undefined") {
                //question.init();
            }
        });
    }
};

// Execute inital page
initpage.init();
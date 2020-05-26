$(document).ready(function () {
    $('#pageLoading').hide();
    $('#pageLoading').removeClass('pageLoading');

    $('#lnkLogin').click(function (e) {
        $("#loginModal").modal();
    });

    $( document ).ajaxStart(function() {
        //$('body').loadingModal('animation', 'fadingCircle').loadingModal('backgroundColor', 'gray');
        $('#pageLoading').addClass('pageLoading');
        $('#pageLoading').show();        
    });

    $(document).ajaxComplete(function (event, request, settings) {
        //$('body').loadingModal('hide');
        //$('body').loadingModal('destroy');
        $('#pageLoading').hide();
        $('#pageLoading').removeClass('pageLoading');
    });

    Handlebars.registerHelper({
        eq: function (v1, v2) {
            return v1 === v2;
        },
        ne: function (v1, v2) {
            return v1 !== v2;
        },
        lt: function (v1, v2) {
            return v1 < v2;
        },
        gt: function (v1, v2) {
            return v1 > v2;
        },
        lte: function (v1, v2) {
            return v1 <= v2;
        },
        gte: function (v1, v2) {
            return v1 >= v2;
        },
        and: function () {
            return Array.prototype.slice.call(arguments).every(Boolean);
        },
        or: function () {
            return Array.prototype.slice.call(arguments, 0, -1).some(Boolean);
        }
    });
});
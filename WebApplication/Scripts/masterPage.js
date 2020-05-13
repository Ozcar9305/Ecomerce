$(document).ready(function () {

    $('#lnkLogin').click(function (e) {
        $("#loginModal").modal();
    });

    $( document ).ajaxStart(function() {
        $('body').loadingModal('animation', 'fadingCircle').loadingModal('backgroundColor', 'gray');
    });

    $(document).ajaxComplete(function (event, request, settings) {
        $('body').loadingModal('hide');
        $('body').loadingModal('destroy');
    });
});
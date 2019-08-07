$(".cart-summary").on('click', function () {
    $('#cart-first').removeClass("wizard-step-active");
    $('#cart-second').addClass("wizard-step-active");

    $('#first-form').fadeOut(200, function () {
        $('#second-form').fadeIn(200);
    });
});
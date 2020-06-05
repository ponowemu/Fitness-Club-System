$(".cart-summary").on('click', function () {
    $('#cart-first').removeClass("wizard-step-active");
    $('#cart-second').addClass("wizard-step-active");

    $('#first-form').fadeOut(200, function () {
        $('#second-form').fadeIn(200);
    });
});

$(".cart-payment").on('click', function () {
    $('#cart-first').removeClass("wizard-step-active");
    $('#cart-second').removeClass("wizard-step-active");
    $('#cart-third').addClass("wizard-step-active");

    $('#second-form').fadeOut(200, function () {
        $('#third-form').fadeIn(200);
    });
});

$('.cart-pay').on('click', function () {

    var url = 'https://sklep.przelewy24.pl/zakup.php?';
    url += 'z24_id_sprzedawcy=89636&';
    url += 'z24_kwota=' + $('#totalPrice').val() + '&';
    url += 'z24_crc=a00da06a948e953d&';
    url += 'z24_nazwa=Zakupy trimfit.pl&';
    url += 'z24_return_url=http://app.trimfit.pl/Cart/Summary';

    window.location.href = url;
});
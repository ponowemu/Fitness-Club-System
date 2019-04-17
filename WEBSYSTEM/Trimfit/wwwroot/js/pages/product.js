


$(document).ready(function () {
    $('.single-product').on('click', function () {

        $.ajax({
            url: '/Product/AddToCart/',
            type: 'POST',
            dataType: 'json',
            data: {
                product_id: $(this).attr('id').split('-')[1]
            },
            success: function (msg) {
                swal('Dodano do koszyka', 'Produkt pomyślnie dodany do koszyka', 'success');
            },
            error: function (msg) {
                alert(msg);
            }
        });

    });
});





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

$("#product_gross_price").on('input', function () {
    var gross_price = $(this).val();
    var net_price = gross_price / 1.23;

    $('#product_net_price').val(net_price);
});

$(".product_icon").on('click', function () {
    console.log($(this));
    $('.product_icon').attr('checked',null);
    $(this).attr('checked', 'checked');
});

$(".product-needs-validation").submit(function () {
    var form = $(this);
    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();

    }
    else {
        $("#loader-wrapper").fadeIn();
        event.preventDefault();

        var product_name = $("#product_name").val();
        var product_gross_price = $("#product_gross_price").val();
        var product_net_price = $("#product_net_price").val();
        var product_clubs_list = $("#clubs_list").val();
        var product_categories_list = $("#categories_list").val();
        var product_icon = $("input[name='product_icon']:checked").val();
        var product_quantity = $("#product_quantity").val();
        var product_status = $("#product_status").val();

        $.ajax({
            url: "/Product/PostProduct",
            type: 'POST',
            dataType: 'json',
            data: {
                product_name: product_name,
                club_id: product_clubs_list,
                product_gross_price: product_gross_price,
                product_net_price: product_net_price,
                category_id: product_categories_list,
                product_icon: product_icon,
                product_quantity: product_quantity,
                product_status: product_status
            },
            success: function (data) {
                $("#loader-wrapper").fadeOut();
                swal('Produkt dodany', 'Pomyślnie dodano nowy produkt!', 'success');

            },
            error: function (data) {
                $("#loader-wrapper").fadeOut();
            }
        });

    }
    form.addClass('was-validated');
});
﻿

// obsługujem CRUD'a na koszyku
$(document).ready(function () {
    $('.single-product').on('click', function () {

        $.ajax({
            url: '/Cart/AddToCart/',
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

    $('.element_quantity').on('input', function () {
        var value = $(this).val();
        var product_id = $(this).prev('input').val();
        $.ajax({
            url: '/Cart/ChangeQuantityFromCart/',
            type: 'POST',
            dataType: 'json',
            data: {
                product_id: product_id,
                quantity: value
            },
            success: function (msg) {
                location.reload();
            },
            error: function (msg) {
                alert(msg);
            }
        });
    });

    $('.element_delete').on('click', function () {
        var product_id = $(this).prev('input').val();
        swal({
            title: 'Jesteś pewien?',
            text: 'Czy na pewno chcesz usunąć ten produkt z koszyka?',
            icon: 'warning',
            buttons: true,
            dangerMode: true
        })
            .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        url: '/Cart/RemoveFromCart/',
                        dataType: 'json',
                        type: 'post',
                        data:
                        {
                            product_id: product_id
                        },
                        success: function (msg) {
                            swal('Poof! Your imaginary file has been deleted!', {
                                icon: 'success',
                            });
                            location.reload();
                        },
                        error: function (msg) {
                            alert(msg);
                        }
                    });

                }
            });
    });
});


$(".product_icon").on('click', function () {
    $('.product_icon').attr('checked', null);
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

$('.delete-product').on('click', function () {
    var pid = $(this).data('id');
    swal({
        title: 'Jesteś pewien?',
        text: 'Czy na pewno chcesz usunąć ten produkt?',
        icon: 'warning',
        buttons: true,
        dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: '/Product/DeleteAsync',
                    method: 'DELETE',
                    data: {
                        productId: pid
                    },
                    success: function () {
                        swal('Poof! Produkt został usunięty!', {
                            icon: 'success'
                        });
                        location.reload();
                    },
                    error: function () {
                        swal('Ajajaj! Nie udało się usunąc produktu.', {
                            icon: 'error'
                        });
                    }
                });
            }
        });
   
});


$(".product-needs-validation-edit").submit(function () {
    var form = $(this);
    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();

    }
    else {
        $("#loader-wrapper").fadeIn();
        event.preventDefault();

        var product_id = $('#product_id').val();
        var product_name = $("#product_name").val();
        var product_gross_price = $("#product_gross_price").val();
        var product_net_price = $("#product_net_price").val();
        var product_clubs_list = $("#clubs_list").val();
        var product_categories_list = $("#categories_list").val();
        var product_icon = $("input[name='product_icon']:checked").val();
        var product_quantity = $("#product_quantity").val();
        var product_status = $("#product_status").val();

        $.ajax({
            url: "/Product/PutProduct",
            type: 'PUT',
            dataType: 'json',
            data: {
                product_id: product_id,
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
                swal('Produkt zapisany', 'Pomyślnie zapisano produkt!', 'success');

            },
            error: function (data) {
                $("#loader-wrapper").fadeOut();
            }
        });

    }
    form.addClass('was-validated');
});
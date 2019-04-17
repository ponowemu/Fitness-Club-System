"use strict";


$("#table-service").dataTable({
    "columnDefs": [
        { "sortable": true, "targets": [2, 3] }
    ]
});




//FORMULARZ DODAWANIA NOWEJ USŁUGI 
// WALIDACJA
// WYSYŁANIE DANYCH
// AJAX 
// DYNAMICZNE POLA

$("#service_gross_price").on('input', function () {
    var gross_price = $(this).val();
    var net_price = gross_price / 1.23;

    $('#service_net_price').val(net_price);
});

$(".service-needs-validation").submit(function () {
    var form = $(this);
    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();

    }
    else {
        $("#loader-wrapper").fadeIn();
        event.preventDefault();
        var service_duration = $("#service_duration").val();
        var service_name = $("#service_name").val();
        var service_description = $("#summernote-new-service").val();
        var service_gross_price = $("#service_gross_price").val();
        var service_net_price = $("#service_net_price").val();
        var service_employees_list = $("#employees_list").val();
        var service_clubs_list = $("#clubs_list").val();
        var service_categories_list = $("#categories_list").val();

        var mon_from = $("#mon_from").val();
        var mon_to = $("#mon_to").val();

        var tue_from = $("#tue_from").val();
        var tue_to = $("#tue_to").val();

        var wen_from = $("#wen_from").val();
        var wen_to = $("#wen_to").val();

        var thu_from = $("#thu_from").val();
        var thu_to = $("#thu_to").val();
        var fri_from = $("#fri_from").val();
        var fri_to = $("#fri_to").val();
        var sat_from = $("#sat_from").val();
        var sat_to = $("#sat_to").val();
        var sun_from = $("#sun_from").val();
        var sun_to = $("#sun_to").val();


        $.ajax({
            url: "/Service/PostService",
            type: 'POST',
            dataType: 'json',
            data: {
                service_duration: service_duration,
                service_name: service_name,
                service_description: service_description,
                service_clubs_list: service_clubs_list,
                service_employees_list: service_employees_list,
                service_gross_price: service_gross_price,
                service_net_price: service_net_price,
                category_id: service_categories_list,
                mon_from: mon_from,
                mon_to: mon_to,
                tue_from: tue_from,
                tue_to: tue_to,
                wen_from: wen_from,
                wen_to: wen_to,
                thu_from: thu_from,
                thu_to: thu_to,
                fri_from: fri_from,
                fri_to: fri_to,
                sat_from: sat_from,
                sat_to: sat_to,
                sun_from: sun_from,
                sun_to: sun_to
            },
            success: function (data) {
                $("#loader-wrapper").fadeOut();
                swal('Usługa utworzona', 'Pomyślnie dodano nową usługę!', 'success');

            },
            error: function (data) {
                $("#loader-wrapper").fadeOut();
            }
        });

    }
    form.addClass('was-validated');
});

$('#summernote-new-service').summernote({
    placeholder: 'Wprowadź krótki opis oferty.',
    tabsize: 3,
    height: 100,
    toolbar: [
        ['para', ['style', 'ul', 'ol', 'paragraph']],
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],
    ]
});


var cleaveB = new Cleave('.service-duration-time', {
    numericOnly: true,
    blocks: [3]
});


var cleaveC = new Cleave('.service-gross-price', {
    numeral: true,
    numeralThousandsGroupStyle: 'none',
    numeralPositiveOnly: true
});


// Select2
if (jQuery().select2) {
    $(".select2").select2();
}


"use strict";


// obsługa uzupełniania 
// lista kategorii
// lista klubów
// lista pracowników

$.ajax({
    url: '/Service/GetEmployees',
    method: 'GET',
    data: { clubId: 1 },
    success: function (data) {
        var idsA = $('#selectedEmployees').val().toString().split(',');
        let ids = idsA.map(function (item) {
            return parseInt(item, 10);
        });
        data.forEach(function (d) {
            if (ids.includes(d.employee_id))
                $('#employees_list').append('<option selected value="' + d.employee_id + '">' + d.employee.employee_firstname + ' ' + d.employee.employee_lastname + '</option>');
            else
                $('#employees_list').append('<option value="' + d.employee_id + '">' + d.employee.employee_firstname + ' ' + d.employee.employee_lastname + '</option>');
        });
    }
});

$.ajax({
    url: '/Service/GetCategories',
    method: 'GET',
    success: function (data) {
        var idsA = $('#selectedCategories').val().toString().split(',');
        let ids = idsA.map(function (item) {
            return parseInt(item, 10);
        });
        data.forEach(function (d) {
            if (ids.includes(d.category_id))
                $('#categories_list').append('<option selected value="' + d.category_id + '">' + d.category_name + '</option>');
            else
                $('#categories_list').append('<option value="' + d.category_id + '">' + d.category_name + '</option>');
        });
    }
});

$.ajax({
    url: '/Service/GetClubs',
    method: 'GET',
    success: function (data) {
        var idsA = $('#selectedClubs').val().toString().split(',');
        let ids = idsA.map(function (item) {
            return parseInt(item, 10);
        });
        data.forEach(function (d) {
            if (ids.includes(d.club_id))
                $('#clubs_list').append('<option selected value="' + d.club_id + '">' + d.club_name + '</option>');
            else
                $('#clubs_list').append('<option value="' + d.club_id + '">' + d.club_name + '</option>');
        });
    }
});

$("#table-service").dataTable({
    "columnDefs": [
        { "sortable": true, "targets": [2, 3] }
    ]
});


$('.delete-service').on('click', function () {
    var id = $(this).closest('td').find('input[type="hidden"]').val();
    $.ajax({
        url: '/Service/Delete',
        method: 'DELETE',
        data:
        {
            serviceId: id
        },
        success: function (e) {
            swal('Usługa usunięta', 'Pomyślnie usunięto twoją usługę!', 'success').then((value) => {
                location.reload();
            });
        },
        error: function () {
            swal('Błąd', 'Nie udało się usunąć usługi. Skontaktuj się z administratorem!', 'error').then((value) => {
                location.reload();
            });
        }
    });
});


//FORMULARZ DODAWANIA NOWEJ USŁUGI 
// WALIDACJA
// WYSYŁANIE DANYCH
// AJAX 
// DYNAMICZNE POLA


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



// Proces rezerwacji danej usługi
// 
// Generalnie tak:
// 1) wybieramy masażyk
// 2) otwiera się nam popup na którym dopinamy szczegóły
// 2.1) wybieramy datę, system sprawdza, itd. 
// 2.2) end-point user dostaje liste proponowanych terminów
// 3) usługa jest dodawana do koszyka ze wszystkimi szczegółami

$(document).ready(function () {

    $(".book_service").on('click', function (event) {
        event.preventDefault();
        var service_id = $(this).attr('data-serviceid');
        //alert(service_id);
        $('.book_service_date').daterangepicker({
            locale: { format: 'YYYY-MM-DD hh:mm' },
            singleDatePicker: true,
            timePicker: true,
            timePicker24Hour: true,
            parentEl: $("#book_service_modal")
        });

        $('#book_service_modal').modal({
            show: true
        });

        $('.book_service_date').on('apply.daterangepicker', function (ev, picker) {
            var timestamp = picker.startDate.format("YYYY-MM-DD HH:mm:ss");
            // here we have to check to availability of service
            $.ajax({
                url: "/Service/CheckAvailability/",
                type: "GET",
                data: {
                    date: timestamp,
                    service_id: service_id  
                },
                success: function (response) {
                    if (response === "200") {
                        alert("Można");
                    }
                    else {
                        alert("Nie można");
                    }
                },
                error: function (response) {
                    swal('Błąd', 'W tej chwili nie możemy sprawdzić dostępności danej usługi. Przepraszamy!', 'error');
                }
            });
        });
    });

});
// koniec rezerwacji


var cleaveB = new Cleave('.service-duration-time', {
    numericOnly: true,
    blocks: [3]
});


// Select2
if (jQuery().select2) {
    $(".select2").select2();
}



/// 
/// zapisywanie
///
$(".service-needs-validation-edit").submit(function () {
    var form = $(this);
    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();

    }
    else {
        $("#loader-wrapper").fadeIn();
        event.preventDefault();
        var service_id = $('#service_id').val();
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
            url: "/Service/PutService",
            type: 'PUT',
            dataType: 'json',
            data: {
                service_id: service_id,
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
                swal('Usługa zapisana', 'Pomyślnie zapisano dane usługi!', 'success');

            },
            error: function (data) {
                $("#loader-wrapper").fadeOut();
                swal('Błąd', 'Nie udało się zapisać danych usługi. Skontaktuj się z administratorem!', 'error').then((value) => {
                    location.reload();
                });
            }
        });

    }
    form.addClass('was-validated');
});
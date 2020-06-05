$('.daterange-cus').daterangepicker({
    locale: { format: 'YYYY-MM-DD' },
    drops: 'down',
    opens: 'right'
});

$('#table-vouchers').dataTable({});

$('#modal-add-new-voucher').on('submit', function (event) {
    var form = $(this);
    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
    }
    else {
        event.preventDefault();

        let acList = $('#activities_list').val();
        let activities = acList.map(function (item) {
            return parseInt(item, 10);
        });
        var date = $('.daterange-cus').val();
        var startDate = date.split(' - ')[0];
        var endDate = date.split(' - ')[1];

        var voucher = {
            voucherDaysNumber: $('#voucher_days').val(),
            voucher_type_id: $('#voucher_type_list').val(),
            voucher_description: $('#summernote-new-service').val(),
            voucher_status: 1,
            voucher_entries_number: $('#voucher_entries').val(),
            voucher_max_suspend_days: $('#voucher_freeze_days').val(),
            voucher_max_suspend_times: $('#voucher_freeze_numbers').val(),
            voucher_net_price: $('#service_net_price').val().replace('.', ','),
            voucher_gross_price: $('#service_gross_price').val().replace('.', ','),
            activitiy_id: activities,
            voucher_timelimit_mon: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_tue: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_wed: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_thu: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_fri: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_sat: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_sun: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"]
        };

        if ($('#voucher_limit').prop('checked') === true) {
            voucher.voucher_infinity = true;
        }
        else {
            voucher.voucher_infinity = false;
            voucher.voucher_startdate = startDate.trim();
            voucher.voucher_enddate = endDate.trim();
        }
        console.log(voucher);
        $.ajax({
            url: '/Voucher/AddNewVoucher',
            type: 'POST',
            dataType: 'json',
            data: voucher,
            success: function (e) {
                $("#loader-wrapper").fadeOut();
                swal('Karnet utworzony', 'Pomyślnie dodano nowy karnet!', 'success');
            },
            error: function (e) {
                $("#loader-wrapper").fadeOut();
                swal('Nieoczekiwany błąd', 'Niestety nie udało się dodać karnetu!', 'error');

            }
        });
    }
    form.addClass('was-validated');
});

$(".delete-voucher").on('click', function () {
    var id = $(this).closest('td').find('input[type="hidden"]').val();


    // sprawdzamy czy ktoś ma kupiony ten karnet,
    var customerNo = 0;
    $.ajax({
        url: '/Voucher/GetVoucherCustomers',
        method: 'GET',
        data: {
            voucherId: id
        },
        success: function (data) {
            console.log(data);
            if (data.length > 0)
                customerNo++;
        }
    }).done(function () {
        if (customerNo === 0) {
            swal({
                title: 'Jesteś pewien, że chcesz usunąc ten karnet?',
                text: 'Po usunięciu nie będziesz mógł przywrócić zmian!',
                icon: 'warning',
                buttons: true,
                dangerMode: true
            })
                .then((willDelete) => {
                    if (willDelete) {
                        $.ajax({
                            url: "/Voucher/DeleteAsync/",
                            method: "DELETE",
                            data: { id: id },
                            success: function () {
                                swal('Karnet został usunięty!', {
                                    icon: 'success'
                                }).then((value) => { location.reload(); });
                            },
                            error: function () {
                                swal('Coś poszło nie tak!', { icon: 'error' });
                            }
                        });

                    } else {
                        swal('Karnet nie został usunięty!');
                    }
                });
        }
        else {
            swal({
                title: 'Nie można usunąć karnetu!',
                text: 'W klubie istnieją klienci, którzy korzystają z tego karnetu',
                icon: 'error',
                buttons: false
            });
        }
    });
});

$(".edit-voucher").on('click', function () {
    $("#loader-wrapper").fadeIn();

    var triggerClass = '';
    $(this).removeClass(function () { // Select the element divs which has class that starts with some-class-
        var className = this.className.match(/trigger--fire-modal-\d+/); //get a match to match the pattern some-class-somenumber and extract that classname
        if (className) {
            triggerClass = className[0]; //if it is the one then push it to array
        }
    });
    triggerClass = triggerClass.replace('trigger--', '#');


    $.ajax({
        url: "/Voucher/GetVoucher",
        data:
        {
            voucherId: $(this).prev('#voucher_id').val()
        },
        method: "GET",
        success: function (data) {
            $(triggerClass).find('.voucher_id').val(data.voucher.voucher_id);
            $(triggerClass).find('.voucher_type_list').html('');
            $(triggerClass).find('.activities_list').html('');

            if (data.voucher.voucher_type_id == 1) {
                $(triggerClass).find('.voucher_days').prop('disabled', false);
                $(triggerClass).find('.voucher_entries').prop('disabled', false);
            }
            else if (data.voucher.voucher_type_id == 2) {
                $(triggerClass).find('.voucher_days').prop('disabled', false);
                $(triggerClass).find('.voucher_entries').prop('disabled', true);
            }
            else if (data.voucher.voucher_type_id == 3) {
                $(triggerClass).find('.voucher_entries').prop('disabled', false);
                $(triggerClass).find('.voucher_days').prop('disabled', true);

            }
            data.voucherTypes.forEach(function (d) {
                if (data.voucher.voucher_type_id === d.voucher_type_id)
                    $(triggerClass).find('.voucher_type_list').append('<option selected value="' + d.voucher_type_id + '">' + d.voucher_type_name + '</option>');
                else
                    $(triggerClass).find('.voucher_type_list').append('<option value="' + d.voucher_type_id + '">' + d.voucher_type_name + '</option>');
            });
            data.activities.forEach(function (d) {
                if (data.voucher.activitiy_id !== null && data.voucher.activitiy_id.includes(d.activity_id))
                    $(triggerClass).find('.activities_list').append('<option selected value="' + d.activity_id + '">' + d.activity_name + '</option>');
                else
                    $(triggerClass).find('.activities_list').append('<option value="' + d.activity_id + '">' + d.activity_name + '</option>');
            });

            $(triggerClass).find('.voucherDateRange').daterangepicker({ startDate: new Date(data.voucher.voucher_startdate), endDate: new Date(data.voucher.voucher_enddate) });

            $(triggerClass).find('.voucher_days').val(data.voucher.voucherDaysNumber);
            $(triggerClass).find('.summernote-edit-voucher').val(data.voucher.voucher_description);
            $(triggerClass).find('.voucher_entries').val(data.voucher.voucher_entries_number);
            $(triggerClass).find('.voucher_freeze_numbers').val(data.voucher.voucher_max_suspend_times);
            $(triggerClass).find('.voucher_freeze_days').val(data.voucher.voucher_max_suspend_days);
            $(triggerClass).find('.summernote-new-service').text(data.voucher.voucher_description);
            $(triggerClass).find('.voucher_gross_price').val(data.voucher.voucher_gross_price);
            $(triggerClass).find('.voucher_net_price').val(data.voucher.voucher_net_price);


        },
        error: function () {

        }
    }).done(function () {
        $("#loader-wrapper").fadeOut();
    });
});
//////////////////////////////
// ============= EDIT ========
//////////////////////////////
$(".edit-voucher").fireModal({
    title: 'Edytuj karnet',
    body: $("#modal-edit-voucher"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {
        var fm = $(this);

        if (fm[0].checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
            form.stopProgress();
        }
        else {
            e.preventDefault();

            $(form).find('.voucher_days').val();

            var activities = null;
            let acList = $(fm).find('.activities_list').val();
            if (acList != '') {
                activities = acList.map(function (item) {
                    return parseInt(item, 10);
                });
            }

            var voucher = {
                voucher_id: $(fm).find('.voucher_id').val(),
                voucherDaysNumber: $(fm).find('.voucher_days').val(),
                voucher_type_id: $(fm).find('.voucher_type_list').val(),
                voucher_description: $(fm).find('.summernote-edit-voucher').val(),
                voucher_status: 1,
                voucher_entries_number: $(fm).find('.voucher_entries').val(),
                voucher_max_suspend_days: $(fm).find('.voucher_freeze_days').val(),
                voucher_max_suspend_times: $(fm).find('.voucher_freeze_numbers').val(),
                voucher_net_price: $(fm).find('.voucher_net_price').val().replace('.', ','),
                voucher_gross_price: $(fm).find('.voucher_gross_price').val().replace('.', ','),
                activitiy_id: activities
            };

            if ($(fm).find('.voucher_limit').prop('checked') === true) {
                voucher.voucher_infinity = true;
            }
            else {
                var date = $(fm).find('.voucherDateRange').val();
                var startDate = date.split(' - ')[0];
                var endDate = date.split(' - ')[1];

                voucher.voucher_infinity = false;
                voucher.voucher_startdate = startDate.trim();
                voucher.voucher_enddate = endDate.trim();
            }

            $.ajax({
                url: '/Voucher/UpdateVoucher',
                type: 'PUT',
                dataType: 'json',
                data: voucher,
                success: function (e) {
                    $("#loader-wrapper").fadeOut();
                    swal('Karnet utworzony', 'Pomyślnie zapisano karnet!', 'success').then((value) => { location.reload(); });
                },
                error: function (e) {
                    $("#loader-wrapper").fadeOut();
                    swal('Nieoczekiwany błąd', 'Niestety nie udało się zapisać karnetu!', 'error');

                }
            });
        }
        fm.addClass('was-validated');
        e.preventDefault();
    },
    shown: function (modal, fm) {
        console.log(fm);
    },
    buttons: [
        {
            text: 'Zapisz',
            submit: true,
            class: 'btn btn-primary btn-shadow',
            handler: function (modal) {
            }
        }
    ]
});

$('.summernote-edit-voucher').summernote({
    placeholder: 'Wprowadź krótki opis oferty.',
    tabsize: 3,
    height: 100,
    toolbar: [
        ['para', ['style', 'ul', 'ol', 'paragraph']],
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],
    ]
});

///////////////////////////////
// =========== CART ===========

$('.addVoucherToCart').on('click', function () {

    var voucherId = $(this).data('voucher-id');


    $.ajax({
        url: '/Cart/AddToCart/',
        type: 'POST',
        dataType: 'json',
        data: {
            voucher_id: voucherId
        },
        success: function (msg) {
            swal('Dodano do koszyka', 'Karnet został pomyślnie dodany do koszyka', 'success');
        },
        error: function (msg) {
            alert(msg);
        }
    });

});

$(document).ready(function () {
    $('#voucher_type_list').on('change', function () {

        $('#voucher_entries').prop("disabled", false);
        $('#voucher_days').prop("disabled", false);
        if (this.value == 1) {
            $('#voucher_entries').prop("disabled", false);
            $('#voucher_days').prop("disabled", false);
        }
        else if (this.value == 2) {
            $('#voucher_entries').attr('disabled', 'disabled');
        }
        else if (this.value == 3) {
            $('#voucher_days').attr('disabled', 'disabled');
        }
    });
    $('#voucher_limit').on('change', function () {
        if ($('.daterange-cus').prop('disabled') === true)
            $('.daterange-cus').prop('disabled', false);
        else
            $('.daterange-cus').prop('disabled', true);
    });

    // Update formularza na karcie edycji vouchera
    $('.voucher_type_list').on('change', function () {
        $('.voucher_entries').prop("disabled", false);
        $('.voucher_days').prop("disabled", false);
        if (this.value == 1) {
            $('.voucher_entries').prop("disabled", false);
            $('.voucher_days').prop("disabled", false);
        }
        else if (this.value == 2) {
            $('.voucher_entries').attr('disabled', 'disabled');
        }
        else if (this.value == 3) {
            $('.voucher_days').attr('disabled', 'disabled');
        }
    });
    $('.voucher_limit').on('change', function () {
        if ($('.voucherDateRange').prop('disabled') === true)
            $('.voucherDateRange').prop('disabled', false);
        else
            $('.voucherDateRange').prop('disabled', true);
    });

    
});

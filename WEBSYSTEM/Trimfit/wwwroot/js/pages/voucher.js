$('.daterange-cus').daterangepicker({
    locale: { format: 'YYYY-MM-DD' },
    drops: 'down',
    opens: 'right'
});

$('#modal-add-new-voucher').on('submit', function (e) {
    console.log(e);
    var form = $(this);

    if (form[0].checkValidity() === false) {
        event.preventDefault();
        event.stopPropagation();
    }
    else {
        var date = $('.daterange-cus').val();
        var startDate = date.split(' - ')[0];
        var endDate = date.split(' - ')[1];

        alert($('#activities_list').val());


        var voucher = {
            voucher_startdate: startDate.trim(),
            voucher_enddate: endDate.trim(),
            voucher_type_id: $('#voucher_type_list').val(),
            voucher_description: $('#summernote-new-service').val(),
            voucher_status: 1,
            voucher_entries_number: $('#voucher_entries').val(),
            voucher_infinity: false,
            voucher_max_suspend_days: $('#voucher_freeze_days').val(),
            voucher_max_suspend_times: $('#voucher_freeze_numbers').val(),
            voucher_net_price: $('#service_net_price').val(),
            voucher_gross_price: $('#service_gross_price').val(),
            activity_id: [$('#activities_list').val()],
            voucher_timelimit_mon: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_tue: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_wed: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_thu: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_fri: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_sat: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"],
            voucher_timelimit_sun: [$('#mon_from').val() + ":00", $('#mon_to').val() + ":00"]
        };

        $.ajax({
            url: '/Voucher/AddNewVoucher',
            type: 'POST',
            data: voucher,
            success: function (e) {
                $("#loader-wrapper").fadeOut();
                swal('Karnet utworzony', 'Pomyślnie dodano nowy karnet!', 'success');
            },
            error: function (e) {
                $("#loader-wrapper").fadeOut();
            }
        });
    }
    form.addClass('was-validated');




});
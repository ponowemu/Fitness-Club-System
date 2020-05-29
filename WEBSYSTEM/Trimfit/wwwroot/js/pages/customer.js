$("#add-new-customer").fireModal({
    title: 'Dodaj nowego klienta',
    body: $("#modal-new-customer"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {
        var fm = $(this);

        if (fm[0].checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
            form.stopProgress();
        }
        else {

            var address = {
                address_1: $("#customer_street").val(),
                address_2: $('#customer_homenumber').val(),
                address_city: $('#customer_city').val(),
                address_country: "Polska",
                address_email: $('#customer_email').val(),
                address_postcode: $('#customer_postcode').val(),
                address_phone: $('#customer_phonenumber').val()
            };

            var customer = {
                customer_firstname: $("#customer_name").val(),
                customer_lastname: $('#customer_surname').val(),
                customer_birthdate: $('#customer_birthdate').val(),
                customer_gender: $('#customer_gender').val(),
                customer_status: $('#customer_status').val(),
                customer_display_name: $('#customer_displayname').val(),
                customer_isconfirmed: false,
                customer_photo_url: "",
                address: address,
                user_id: null
            };

            // i przekazujemy ajaxem
            $.ajax({
                url: "/Customer/AddAsync",
                method: "POST",
                data: customer,
                success: function () {
                    modal.find('.modal-body').prepend('<div class="alert alert-success">Użytkownik został dodany</div>');
                    form.stopProgress();
                },
                error: function () {
                    modal.find('.modal-body').prepend('<div class="alert alert-error">Error occured. Contact with administrator!</div>');
                    form.stopProgress();
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
            text: 'Dodaj',
            submit: true,
            class: 'btn btn-primary btn-shadow',
            handler: function (modal) {
            }
        }
    ]
});

$("#customers_list").dataTable({
    "ajax":
    {
        "url": "/Customer/GetCustomers",
        "async": "false"
    },
    "columnDefs": [
        {
            'targets': 0,
            'data': 'customer_Id'
        },
        {
            'targets': 1,
            'render': function (data, type, row) {
                if (row.customer_Photo_Url !== '')
                    return '<img src="' + row.customer_Photo_Url + '" width="45" class="rounded-circle" />';
                else
                    return '<img src="/images/stisla/avatar/avatar-3.png" width="45" class="rounded-circle" />';
            }
        },
        {
            'targets': 2,
            'render': function (data, type, row) {
                return row.customer_Firstname + ' ' + row.customer_Lastname;
            }
        },
        {
            'targets': 3,
            'render': function (data, type, row) {
                let html = '';

                html += '<div class="row">';
                if (row.customer_Status === 1)
                    html += '<div class="badge badge-active">Status: Aktywny</div>';
                else
                    html += '<div class="badge badge-inactive">Status: Nieaktywny</div>';
                html += '</div><div class="row mt-1">';
                if (row.customer_Isconfirmed === true)
                    html += '<div class="badge badge-active">Zweryfikowany: Tak</div>';
                else
                    html += '<div class="badge badge-inactive">Zweryfikowany: Nie</div>';
                html += '</div>';
                return html;
            }
        },
        {
            'targets': 4,
            'data': 'customer_Birthdate',
            'render': function (data, type, row) {
                return moment(data).format('MM/DD/YYYY');
            }
        },
        {
            'targets': 5,
            'data': 'customer_Id'
            // TODO: ostatnie wejście 
        },
        {
            'targets': 6,
            'data':'vouchers',
            'render': function (d, t, r, meta) {
                if (d.length > 0) {
                    d.forEach(function (data) {
                        let voucherStatus = '';

                        if (data.voucher.voucher_Status == 0)
                            voucherStatus = 'info'; // voucher nieaktywny;
                        else if (data.voucher.voucher_Status == 1)
                            voucherStatus = 'success';
                        else
                            voucherStatus = 'warning';

                        let voucher = '<strong>' + data.voucher.voucher_Description + '</strong><br />Od: ' + moment(data.voucher.voucher_Startdate).format('DD/MM/YYYY') +
                            '<br />Do: ' + moment(data.voucher.voucher_Enddate).format('DD/MM/YYYY');
                        response = '<div class="badge badge-' + voucherStatus + '">' + voucher + '</div>';
                    });
                }
                else
                    response = '<p>Brak</p>';

                return response;
            }
        },
        {
            'targets': 7,
            'data': 'customer_Id'
        },
        { "sortable": true, "targets": [2, 3] }
    ]
});
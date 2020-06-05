$(document).ready(function () {
    $("#customers_list").dataTable({
        "ajax":
        {
            "url": "/Customer/GetCustomers",
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
                        return '<img src="/upload/' + row.customer_Photo_Url + '" width="45" class="rounded-circle" />';
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
                    return moment(data).format('DD/MM/YYYY');
                }
            },
            {
                'targets': 5,
                'data': 'customer_Id'
                // TODO: ostatnie wejście 
            },
            {
                'targets': 6,
                'data': 'vouchers',
                'render': function (d, t, r, meta) {
                    let response = '';
                    if (d.length > 0) {
                        d.forEach(function (data) {
                            let voucherStatus = '';

                            if (data.voucher.voucher_Status == 0)
                                voucherStatus = 'info'; // voucher nieaktywny;
                            else if (data.voucher.voucher_Status == 1)
                                voucherStatus = 'success';
                            else
                                voucherStatus = 'warning';
                            let voucher = '';
                            if (data.voucher.voucher_Type_Id === 1)
                                voucher = '<strong>Open</strong>';
                            else if (data.voucher.voucher_Type_Id === 2)
                                voucher = '<strong>Okresowy</strong>';
                            else
                                voucher = '<strong>Ilościowy</strong>';

                            let details = '<strong>Okres:</strong> ' + (moment(data.added).format('DD/MM/YYYY')) + ' - ' + (data.voucherEndDate === null ? '' : moment(data.voucherEndDate).format('DD/MM/YYYY'));
                            details += '<br />';
                            details += '<strong>Pozostało wejść:</strong> ' + (data.freeEntries === null ? 'N/D' : data.freeEntries);
                            details += '<br />';
                            details += '<strong>Identyfikator:</strong> ' + data.voucher.voucher_Id;
                            details += '<br />';
                            details += '<strong>Zamrażalny: </strong>' + (data.voucher.voucher_Max_Suspend_Times > 0 ? 'Tak' : 'Nie');

                            response += '<div class="badge mt-1 ml-1 badge-' + voucherStatus + '" data-toggle="tooltip" data-html="true" data-title="' + details + '">' + voucher + '</div>';
                        });
                    }
                    else
                        response = '<p>Brak</p>';

                    return response;
                }
            },
            {
                'targets': 7,
                'render': function (d, t, row) {
                    let html = '';
                    html += '<button class="btn btn-icon icon-left btn-primary mb-3" onclick="editCustomer(' + row.customer_Id + ')" >' +
                        '<i class="fas fa-pencil-alt"></i> Edytuj</button>' +
                        '<button data-id="'+row.customer_Id+'" class="btn btn-icon icon-left btn-danger mb-3 delete-customer"> <i class="fas fa-trash-alt"></i> Usuń</button >';
                    return html;
                }
            },
            { "sortable": true, "targets": [2, 3] },

        ],
        drawCallback: function (settings) {
            $('[data-toggle="tooltip"]').tooltip();
        }
    });


});
$('.delete-voucher-customer').on('click', function () {
    swal({
        title: 'Jesteś pewien?',
        text: 'Po usunięciu nie będziesz mógł przywrócić zmian!',
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/Voucher/DeleteConnection/",
                    method: "DELETE",
                    data: { id: $(this).data('vc-id') },
                    success: function () {
                        swal('Powiązanie zostało usunięta!', {
                            icon: 'success'
                        }).then((value) => { location.reload(); });
                    },
                    error: function () {
                        swal('Coś poszło nie tak!', { icon: 'error' });
                    }
                });

            } else {
                swal('Powiązanie nie zostało usunięte!');
            }
        });
});
$("#assign-voucher").fireModal({
    title: 'Przypisz karnet',
    body: $("#assign-voucher-modal"),
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

            $.ajax({
                url: '/Voucher/AssignCustomer',
                data: {
                    customer_id: $('#customer-id').val(),
                    voucher_id: $('#voucher_type').val(),
                    start: $('#voucher_start_from').val()
                },
                success: function (e) {
                    console.log("Suk");
                    console.log(e);
                    modal.find('.modal-body').prepend('<div class="alert alert-success">Użytkownik został przypisany do karnetu</div>');
                    form.stopProgress();
                    //  location.reload();
                },
                error: function (e) {

                    modal.find('.modal-body').prepend('<div class="alert alert-danger">Nie udało się przypisać użytkowika.' + e.responseText + '</div>');
                    form.stopProgress();
                    // location.reload();
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




function editCustomer(customerId) {
    window.location.href = '/Customer/Edit/' + customerId;
}

$("#customers_list").on('click', '.delete-customer', function () {
    var id = $(this).data('id');
    //alert(id);
    swal({
        title: 'Jesteś pewien?',
        text: 'Po usunięciu nie będziesz mógł przywrócić zmian!',
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/Customer/DeleteAsync/",
                    method: "DELETE",
                    data: { customerId: id },
                    success: function () {
                        swal('Klient został usunięty!', {
                            icon: 'success'
                        }).then((value) => { location.reload(); });
                    },
                    error: function () {
                        swal('Coś poszło nie tak!', { icon: 'error' });
                    }
                });

            } else {
                swal('Twój klient nie został usunięty i jest bezpieczny!');
            }
        });
});


///////////////
//// EDIT - customer
$('#customerProfile').on('submit', function (e) {
    e.preventDefault();
    var fileUpload = $("#photo").get(0);
    var files = fileUpload.files;
    var formData = new FormData();
    if (files != null && files != '') {
        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            console.log('(files[i].name:' + files[i].name);
            formData.append('formFiles', files[i]);
        }
    }


    var status = 0;
    if ($("#customer_status").prop('checked') === true)
        status = 1;
    else
        status = 0;

    // Ta metoda poniżej zwraca ogólny url, także nawet 
    // w przypadku klienta możemy ją wykorzystac
    var photoUrl = $("#fakePhoto").val();
    if (files != '' && files != null && files.length > 0) {
        $.ajax({
            url: '/Employee/UploadFile',
            method: 'POST',
            processData: false,
            contentType: false,
            data: formData,
            async: false,
            success: function (e) {
                console.log(e);
                if (e.length > 0) {
                    photoUrl = e[0];
                }
            },
            error: function (e) {
                $("#loader-wrapper").fadeOut();
                swal('Nieoczekiwany błąd', 'Niestety nie udało się dodać klienta! Wystąpił błąd podczas wgrywania zdjęcia.', 'error');
            }
        });
    }

    var Address = {
        address_id: $('#address_id').val(),
        address_1: $("#customer_street").val(),
        address_2: $('#customer_number').val(),
        address_city: $('#customer_city').val(),
        address_country: "Polska",
        address_email: $('#customer_email').val(),
        address_postcode: $('#customer_postcode').val(),
        address_phone: $('#customer_phonenumber').val()
    };
    var Customer = {
        customer_id: $('#customer_id').val(),
        customer_firstname: $('#customer_firstname').val(),
        customer_lastname: $('#customer_lastname').val(),
        customer_birthdate: $('#customer_birthdate').val(),
        customer_gender: $('#customer_gender').val(),
        customer_status: status,
        address: Address,
        address_id: Address.address_id,
        customer_photo_url: photoUrl,
        customer_display_name: $('#customer_display').val(),
        position_id: [1]
    };

    $.ajax({
        url: '/customer/Update',
        method: 'PUT',
        async: false,
        data: {
            customer: Customer
        },
        success: function (e) {
            $("#loader-wrapper").fadeOut();
            swal('Klient zaktualizowany', 'Pomyślnie zaktualizowano dane klienta!', 'success').then((value) => { location.reload(); });
        },
        error: function (e) {
            $("#loader-wrapper").fadeOut();
            swal('Nieoczekiwany błąd', 'Niestety nie udało się zapisać klienta!', 'error');
        }
    });

});
//////////////
/////////////
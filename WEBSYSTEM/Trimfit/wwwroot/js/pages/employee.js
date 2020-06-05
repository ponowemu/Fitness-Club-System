$(document).ready(function () {
    var el = $("#employee_list").dataTable({
        "ajax":
        {
            "url": "/Employee/GetEmployees"
        },
        "columnDefs": [
            {
                'targets': 0,
                'data': 'employee.employee_id'
            },
            {
                'targets': 1,
                'render': function (data, type, row) {
                    if (row.employee.employee_photo_url !== '')
                        return '<img src="/upload/' + row.employee.employee_photo_url + '" width="105" class="rounded-circle" />';
                    else
                        return '<img src="/images/stisla/avatar/avatar-3.png" width="45" class="rounded-circle" />';
                }
            },
            {
                'targets': 2,
                'render': function (data, type, row) {
                    return row.employee.employee_firstname + ' ' + row.employee.employee_lastname;
                }
            },
            {
                'targets': 3,
                'data': 'employee.employee_display_name'
            },
            {
                'targets': 4,
                'render': function (data, type, row) {
                    let html = '';

                    html += '<div class="row">';
                    if (row.employee.employee_status === 1)
                        html += '<div class="badge badge-active">Status: Aktywny</div>';
                    else
                        html += '<div class="badge badge-inactive">Status: Nieaktywny</div>';
                    html += '</div>';
                    return html;
                }
            },
            {
                'targets': 5,
                'data': 'employee.employee_birthdate',
                'render': function (data, type, row) {
                    return moment(data).format('MM/DD/YYYY');
                }
            },
            {
                'targets': 6,
                'data': 'employee.activities',
                'render': function (d, t, r) {
                    let html = '';
                    if (d != '' && d != null) {
                        d.forEach(function (ac) {
                            html += '<div class="badge ml-1 mt-1" data-toggle="tooltip" data-original-title="' + ac.activity_description + '" style="color:#fff;background-color: ' + ac.activity_color + '">' + ac.activity_name + '</div>';
                        });
                    }
                    else
                        html = 'Brak przypisanych aktywności.';

                    return html;
                }
                // TODO: powiązane aktywności
            },
            {
                'targets': 7,
                'data': 'employee.address',
                'render': function (d, t, r, meta) {
                    return '<p><strong>' + d.address_email + '<br /> ' + d.address_phone + '</strong></p><p>' + d.address_1 + ' ' + d.address_2 + '<br />' + d.address_city + ' ' + d.address_postcode + '</p>';
                }
            },
            {
                'targets': 8,
                'data': 'employee.employee_added',
                'render': function (data, type, row) {
                    return moment(data).format('MM/DD/YYYY');
                }
            },
            {
                'targets': 9,
                'render': function (d, t, row) {
                    let html = '';
                    html += '<button class="btn btn-icon icon-left btn-primary mb-3" onclick="editEmployee(' + row.employee.employee_id + ')" >' +
                        '<i class="fas fa-pencil-alt"></i> Edytuj</button>' +
                        '<button class="btn btn-icon icon-left btn-danger mb-3 delete-customer"> <i class="fas fa-trash-alt"></i> Usuń</button >';
                    return html;
                }
            },
            { "sortable": true, "targets": [2, 3] }
        ],
        drawCallback: function (settings) {
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
});

function editEmployee(id) {
    window.location.href = "/Employee/Profile/" + id;
}


///////////////////////
//// PRACOWNIK - UPDATE DANYCH
///////////////////////
// TODO: Przy zapisywaniu, nie aktualizuje zdjęcia
// TODO: niepotrzebnie nadpisuje employee_added
// TODO: status na karcie pracownika nie zgadza się z rzeczywistością/
// TODO: Dodać zliczanie aktywności, usług i opinii na karcie pracownika
$('#employeeProfile').on('submit', function (e) {
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
    console.log(files);
    

    var status = 0;
    if ($("#employee_status").val() === "on")
        status = 1;
    else
        status = 0;

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
                swal('Nieoczekiwany błąd', 'Niestety nie udało się dodać pracownika! Wystąpił błąd podczas wgrywania zdjęcia.', 'error');
            }
        });
    }

    var Address = {
        address_id: $('#address_id').val(),
        address_1: $("#employee_street").val(),
        address_2: $('#employee_number').val(),
        address_city: $('#employee_city').val(),
        address_country: "Polska",
        address_email: $('#employee_email').val(),
        address_postcode: $('#employee_postcode').val(),
        address_phone: $('#employee_phonenumber').val()
    };
    var Employee = {
        employee_id: $('#employee_id').val(),
        employee_firstname: $('#employee_firstname').val(),
        employee_lastname: $('#employee_lastname').val(),
        employee_birthdate: $('#employee_birthdate').val(),
        employee_gender: $('#employee_gender').val(),
        employee_status: status,
        address: Address,
        address_id: Address.address_id,
        employee_photo_url: photoUrl,
        employee_display_name: $('#employee_display').val(),
        position_id: [1]
    };

    $.ajax({
        url: '/Employee/Update',
        method: 'PUT',
        async:false,
        data: {
            employee: Employee
        },
        success: function (e) {
            $("#loader-wrapper").fadeOut();
            swal('Pracownik zaktualizowany', 'Pomyślnie zaktualizowano dane pracownika!', 'success').then((value) => { location.reload(); });
        },
        error: function (e) {
            $("#loader-wrapper").fadeOut();
            swal('Nieoczekiwany błąd', 'Niestety nie udało się zapisać pracownika!', 'error');
        }
    });

});
//////////////////
//// UPDATE - pracownik KONIEC
//////////////////

$('#add-new-employee').fireModal({
    title: 'Dodaj pracownika',
    body: $("#modal-new-employee"),
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

            var fileUpload = $("#photo").get(0);
            var files = fileUpload.files;
            var formData = new FormData();
            // Looping over all files and add it to FormData object  
            for (var i = 0; i < files.length; i++) {
                console.log('(files[i].name:' + files[i].name);
                formData.append('formFiles', files[i]);
            }
            var photoUrl = '';
            $.ajax({
                url: '/Employee/UploadFile',
                method: 'POST',
                processData: false,
                contentType: false,
                data: formData,
                success: function (e) {
                    console.log(e);
                    if (e.length > 0) {
                        photoUrl = e[0];
                    }
                },
                error: function (e) {
                    $("#loader-wrapper").fadeOut();
                    swal('Nieoczekiwany błąd', 'Niestety nie udało się dodać pracownika! Wystąpił błąd podczas wgrywania zdjęcia.', 'error');
                }
            }).done(function (data) {

                var Address = {
                    address_1: $("#employee_street").val(),
                    address_2: $('#employee_homenumber').val(),
                    address_city: $('#employee_city').val(),
                    address_country: "Polska",
                    address_email: $('#employee_email').val(),
                    address_postcode: $('#employee_postcode').val(),
                    address_phone: $('#employee_phonenumber').val()
                };
                var Employee = {
                    employee_firstname: $('#employee_name').val(),
                    employee_lastname: $('#employee_surname').val(),
                    employee_birthdate: $('#employee_birthdate').val(),
                    employee_gender: $('#employee_gender').val(),
                    employee_status: $('#employee_status').val(),
                    address: Address,
                    employee_photo_url: data[0],
                    employee_display_name: $('#employee_displayname').val(),
                    position_id: [1]
                };

                $.ajax({
                    url: '/Employee/Add',
                    method: 'POST',
                    data: {
                        employee: Employee
                    },
                    success: function (e) {
                        $("#loader-wrapper").fadeOut();
                        swal('Pracownik utworzony', 'Pomyślnie dodano pracownika!', 'success').then((value) => { location.reload(); });
                    },
                    error: function (e) {
                        $("#loader-wrapper").fadeOut();
                        swal('Nieoczekiwany błąd', 'Niestety nie udało się dodać pracownika! Wystąpił błąd podczas dodawania pracownika.', 'error');
                    }
                });
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
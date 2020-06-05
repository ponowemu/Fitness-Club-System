$("#activity_list").DataTable({

});

$("#add-new-activity").fireModal({
    title: 'Dodaj nową aktywność',
    body: $("#new-activity"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {
        //alert($('#category_id').val());
        //alert($('#activity_status').val());
        var status = 0;

        if ($("#activity_status").prop('checked') === true)
            status = 1;
        else
            status = 0;

        let form_data = $(e.target).serialize(); // <- to może się przydać
        console.log(form_data);
        $.ajax({
            url: "/Activity/AddAsync",
            method: "POST",
            data: {
                "activity_name": $('#activity_name').val(),
                "activity_description": $('#activity_description').val(),
                "activity_color": $('#activity_color').val(),
                "employee_id": $('#employee_id').val(),
                "category_id": $('#category_id').val(),
                "activity_status": status
            },
            success: function () {
                form.stopProgress();
                swal('Aktywność utworzona', 'Pomyślnie dodano nową aktywność!', 'success').then((value) => {
                    location.reload();
                });
            },
            error: function () {
                form.stopProgress();
                swal('Błąd', 'Nie udało się dodać aktywności. Skontaktuj się z administratorem!', 'error').then((value) => {
                    location.reload();
                });
            }
        });

        e.preventDefault();
    },
    shown: function (modal, form) {
        console.log(form);
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
/// EDYTOWANIE

$('.edit-activity').on('click', function () {
    var acId = $(this).closest('td').find('input[type="hidden"]').val();

    var triggerClass = '';
    $(this).removeClass(function () { // Select the element divs which has class that starts with some-class-
        var className = this.className.match(/trigger--fire-modal-\d+/); //get a match to match the pattern some-class-somenumber and extract that classname
        if (className) {
            triggerClass = className[0]; //if it is the one then push it to array
        }
    });
    triggerClass = triggerClass.replace('trigger--', '#');
    $.ajax({
        url: "/Activity/GetActivity",
        data:
        {
            acId: acId
        },
        method: "GET",
        success: function (data) {

            $(triggerClass).find('.activityId').val(data.activity.activity_id);

            if (data.activity.activity_status === 1)
                $(triggerClass).find('.activity_status').prop('checked', true);
            else
                $(triggerClass).find('.activity_status').prop('checked', false);

            $(triggerClass).find('.activity_name').val(data.activity.activity_name);
            $(triggerClass).find('.activity_color').val(data.activity.activity_color);
            $(triggerClass).find('.activity_description').val(data.activity.activity_description);


            data.employees.forEach(function (d) {
                if (data.activity.employee_id.includes(d.employee_id))
                    $(triggerClass).find('.employee_id').append('<option selected value="' + d.employee_id + '">' + d.employee.employee_firstname + ' ' + d.employee.employee_lastname + '</option>');
                else
                    $(triggerClass).find('.employee_id').append('<option value="' + d.employee_id + '">' + d.employee.employee_firstname + ' ' + d.employee.employee_lastname + '</option>');
            });
            data.categories.forEach(function (d) {
                if (data.activity.category_id.includes(d.category_id))
                    $(triggerClass).find('.category_id').append('<option selected value="' + d.category_id + '">' + d.category_name + '</option>');
                else
                    $(triggerClass).find('.category_id').append('<option value="' + d.category_id + '">' + d.category_name + '</option>');
            });

        },
        error: function (e) {
            swal('Coś poszło nie tak! ' + e, { icon: 'error' });
        }
    }).done(function () {
        $("#loader-wrapper").fadeOut();
    });
});

$(".edit-activity").fireModal({
    title: 'Edytuj aktywność',
    body: $("#edit-activity"),
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
            var status = 0;
            console.log($(fm).find('.employee_id').val());
            if ($(fm).find("#activity_status").prop('checked') === true)
                status = 1;
            else
                status = 0;

            let ep = $(fm).find('.employee_id').val();
            let employees = ep.map(function (item) {
                return parseInt(item, 10);
            });

            let ct = $(fm).find('.category_id').val();
            let categories = ct.map(function (item) {
                return parseInt(item, 10);
            });

            var activity = {
                activity_id: $(fm).find('.activityId').val(),
                activity_name: $(fm).find('.activity_name').val(),
                activity_description: $(fm).find('.activity_description').val(),
                activity_color: $(fm).find('.activity_color').val(),
                employee_id: employees,
                category_id: categories,
                activity_status: status
            };
           
            $.ajax({
                url: "/Activity/UpdateAsync",
                method: "POST",
                data: {
                    activity: activity
                },
                success: function () {
                    form.stopProgress();
                    swal('Aktywność zapisana', 'Pomyślnie zapisano aktywność!', 'success').then((value) => {
                        location.reload();
                    });
                },
                error: function () {
                    form.stopProgress();
                    swal('Błąd', 'Nie udało się zaktualizować aktywności. Skontaktuj się z administratorem!', 'error').then((value) => {
                        location.reload();
                    });
                }
            });

            e.preventDefault();
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

$('.summernote-edit-activity').summernote({
    placeholder: 'Wprowadź krótki opis oferty.',
    tabsize: 3,
    height: 100,
    toolbar: [
        ['para', ['style', 'ul', 'ol', 'paragraph']],
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],
    ]
});



/// EDYTOWANIE
$(".delete-activity").on('click', function () {
    var id = $(this).closest('td').find('input[type="hidden"]').val();
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
                    url: "/Activity/DeleteAsync/",
                    method: "DELETE",
                    data: { id: id },
                    success: function () {
                        swal('Aktywność została usunięta!', {
                            icon: 'success'
                        }).then((value) => { location.reload(); });
                    },
                    error: function () {
                        swal('Coś poszło nie tak!', { icon: 'error' });
                    }
                });

            } else {
                swal('Twoja aktywność nie została usunięta i jest bezpieczna!');
            }
        });
});

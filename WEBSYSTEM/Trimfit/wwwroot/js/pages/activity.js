$("#add-new-activity").fireModal({
    title: 'Dodaj nową aktywność',
    body: $("#new-activity"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {
        //alert($('#category_id').val());
        //alert($('#activity_status').val());
        var status = 0;

        if ($("#activity_status").val() === "on")
            status = 1;
        else
            status = 0;

        let form_data = $(e.target).serialize(); // <- to może się przydać
        console.log(form_data);
        $.ajax({
            url: "/Activity/AddAsync",
            method: "POST",
            data: {
                "activity_name": $('#activity_name').val() ,
                "activity_description": $('#activity_description').val() ,
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
                swal('Błąd', 'Nie udało się dodać kategorii. Skontaktuj się z administratorem!', 'error').then((value) => {
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
                        swal('Coś poszło nie tak!', {icon: 'error'});
                    }
                });
                
            } else {
                swal('Twoja aktywność nie została usunięta i jest bezpieczna!');
            }
        });
});

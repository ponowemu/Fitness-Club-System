"use strict";



$("#add-new-category").fireModal({
    title: 'Dodaj nową kategorię',
    body: $("#new-category"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {

        let form_data = $(e.target).serialize(); // <- to może się przydać
        console.log(form_data);
        $.ajax({
            url: "/Category/AddAsync",
            method: "POST",
            data: {
                "category_name": $('#category_name').val(),
                "category_color": $('#category_color').val()
            },
            success: function () {
                form.stopProgress();
                swal('Kategoria utworzona', 'Pomyślnie dodano nową kategorię!', 'success').then((value) => {
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

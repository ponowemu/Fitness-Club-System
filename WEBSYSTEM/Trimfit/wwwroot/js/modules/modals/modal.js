"use strict";


$("#add-new-timetable").fireModal({
    title: 'Dodaj nowy grafik',
    body: $("#modal-login-part"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {

        let form_data = $(e.target).serialize();
        console.log(form_data);
        // pobieramy aktualne wartoœci z formularza
        var timetable_name = $("#timetable_name").val();
        var timetable_status = $("#timetable_status").val();
        // i przekazujemy ajaxem
        $.ajax({
            url: "/Timetable/AddAsync",
            method: "POST", 
            data: {
                "timetable_name": timetable_name,
                "timetable_status": timetable_status
            }, 
            success: function () {
                modal.find('.modal-body').prepend('<div class="alert alert-success">Grafik zosta³ dodany</div>')
                form.stopProgress();
            },
            error: function () {
                modal.find('.modal-body').prepend('<div class="alert alert-error">Error occured. Contact with administrator!</div>')
                form.stopProgress();
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

"use strict";


$("#table-service").dataTable({
    "columnDefs": [
        { "sortable": true, "targets": [2, 3] }
    ]
});


/*
$("#add-new-service").fireModal({
    title: 'Dodaj nową usługę',
    body: $("#modal-add-new-service"),
    footerClass: 'bg-whitesmoke',
    autoFocus: false,
    onFormSubmit: function (modal, e, form) {

        let form_data = $(e.target).serialize();
        console.log(form_data);
        // pobieramy aktualne wartości z formularza
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
                modal.find('.modal-body').prepend('<div class="alert alert-success">Grafik został dodany</div>');
                form.stopProgress();
            },
            error: function () {
                modal.find('.modal-body').prepend('<div class="alert alert-error">Error occured. Contact with administrator!</div>');
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
*/

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


var cleaveC = new Cleave('.service-gross-price', {
    numeral: true,
    numeralThousandsGroupStyle: 'thousand'
});

// Select2
if (jQuery().select2) {
    $(".select2").select2();
}


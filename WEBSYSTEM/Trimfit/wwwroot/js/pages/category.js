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

/// USUWANIE

$('.delete-category').on('click', function () {
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
                    url: "/Category/DeleteAsync/",
                    method: "DELETE",
                    data: { id: id },
                    success: function () {
                        swal('Kategoria została usunięta!', {
                            icon: 'success'
                        }).then((value) => { location.reload(); });
                    },
                    error: function () {
                        swal('Coś poszło nie tak!', { icon: 'error' });
                    }
                });

            } else {
                swal('Twoja kategoria nie została usunięta i jest bezpieczna!');
            }
        });
});

/// USUWANIE

/// EDYTOWANIE
$(".edit-category").on('click', function () {
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
        url: "/Category/GetCategory",
        data:
        {
            catId: $(this).prev('.category_id').val()
        },
        method: "GET",
        success: function (data) {
            $(triggerClass).find('.category_id').val(data.category_id);
            $(triggerClass).find('.edit_category_name').val(data.category_name);
            $(triggerClass).find('.edit_category_color').val(data.category_color);
        },
        error: function () {

        }
    }).done(function () {
        $("#loader-wrapper").fadeOut();
    });
});


$(".edit-category").fireModal({
    title: 'Edytuj kategorię',
    body: $("#edit-category"),
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

            var category = {
                category_id: $(fm).find('.category_id').val(),
                category_name: $(fm).find('.edit_category_name').val(),
                category_color: $(fm).find('.edit_category_color').val(),
                category_type: 1
            };

            $.ajax({
                url: '/Category/UpdateCategory',
                type: 'PUT',
                dataType: 'json',
                data: { category: category },
                success: function (e) {
                    $("#loader-wrapper").fadeOut();
                    swal('Kategoria zapisana', 'Pomyślnie zapisano kategorię!', 'success').then((value) => { location.reload(); });
                },
                error: function (e) {
                    $("#loader-wrapper").fadeOut();
                    swal('Nieoczekiwany błąd', 'Niestety nie udało się zapisać kategorii!', 'error');

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

$('.summernote-edit-category').summernote({
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

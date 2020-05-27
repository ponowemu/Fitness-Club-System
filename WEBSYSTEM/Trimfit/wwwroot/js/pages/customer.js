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
                user_id : null
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

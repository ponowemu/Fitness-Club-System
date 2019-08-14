Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
};
function getTimetableId() {
    var pathname = window.location.pathname;
    var link = pathname.split("/");

    return link[link.length - 1];
}

function createTimetableActivityModel(timetable_activity_id,activity_id, start_date, end_date, original_color) {

    var color = $("#ta_activity_color").val();
    var repeat = $("#ta_repeatable").val();
    var end = new Date(start_date.toISOString());

    if (end_date === null)
        end.setHours(new Date(start_date.toISOString()).getHours() + 1);
    else
        end = end_date;

    if (color === "#000000")
        color = original_color;

    var reservation_list = false;
    if ($("#ta_reservation_list").is(":checked"))
        reservation_list = true;
    else
        reservation_list = false;

    var t_a = {
        Employee_Id: $("#ta_employee_id").val(),
        Room_Id: $("#ta_room_id").val(),
        Activity_Id: activity_id,
        Timetable_Activity_Day: new Date(start_date.toISOString()).getDay(),
        Timetable_Activity_Starttime: new Date(start_date.getTime() - (start_date.getTimezoneOffset() * 60000)).toISOString(),
        Timetable_Activity_Endtime: new Date(end.getTime() - (end.getTimezoneOffset()*60000)).toISOString(),
        Timetable_Activity_Limit_Places: $("#ta_limit_places").val(),
        Timetable_Activity_Free_Places: $("#ta_limit_places").val(),
        Timetable_Activity_Repeatable: parseInt(repeat),
        Timetable_Activity_Status: "1",
        Timetable_Activity_Reservation_List: reservation_list,
        Timetable_Activity_Color: color,
        Timetable_Id: parseInt(getTimetableId()),
        Timetable_Activity_Id: timetable_activity_id === null ? '0' : timetable_activity_id
    };

    return t_a;
}



document.addEventListener('DOMContentLoaded', function () {

    var TimetableId = getTimetableId();
    var Calendar = FullCalendar.Calendar;
    var Draggable = FullCalendarInteraction.Draggable;

    /* initialize the external events
    -----------------------------------------------------------------*/

    var containerEl = document.getElementById('external-events-list');
    new Draggable(containerEl, {
        itemSelector: '.fc-event',
        eventData: function (eventEl) {
            return {
                title: eventEl.innerText.trim()
            }
        }
    });
    /* initialize the calendar
    -----------------------------------------------------------------*/

    var calendarEl = document.getElementById('calendar');
    var calendar = new Calendar(calendarEl, {
        firstDay: 1,
        allDayDefault: false,
        locale: 'pl',
        minTime: '07:00:00',
        scrollTime: '09:00:00',
        plugins: ['interaction', 'timeGrid'],
        defaultView: 'timeGridWeek',
        displayEventEnd: true,
        forceEventDuration: true,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        slotLabelFormat: {
            hour: 'numeric',
            minute: '2-digit',
            omitZeroMinute: false,
            hour12: false
        },
        editable: true,
        droppable: true,
        eventReceive: function (info) {
            console.log(info);
            // first drop event onto time table
            // here we have to add new record into timetable_activity
            // we will pass whole model to controller
            // new window with some basic details to provide should be shown

            var activity_id = info.draggedEl.dataset.id;
            var original_color = info.draggedEl.dataset.color;
            //console.log(info.draggedEl.dataset.id);

            $.ajax({
                url: "/Activity/GetActivityEmployees/" + activity_id + "/",
                type: "GET",
                success: function (data) {
                    //console.log(data);
                    $("#ta_employee_id").html("");
                    $.each(data, function (index, value) {
                        $("#ta_employee_id").append($("<option />").val(value.employee_id).text(value.employee_display_name));
                    });
                },
                error: function (data) {
                    alert("Sth went wrong. I am really sorry bro!");
                }
            });

            // Nie jestem przekonany co do tych poniższych rozkmin
            // No ale zobaczymy, póki co niech zostanie 
            // zmieniłbym jednak na jakiś callback z tego modala, a nie na click buttona/

            $("#exampleModal").modal({
                show: true
            });

            $(".modal-footer > .btn").unbind('click').click(function (e) {
                e.preventDefault();
                $("#exampleModal").modal('hide');
                console.log(info.event);

                var timetable_activity = createTimetableActivityModel(null, activity_id, info.event.start, info.event.end, original_color);

                $.ajax({
                    url: "/Timetable/AddActivityAsync/",
                    type: "POST",
                    data: JSON.stringify(timetable_activity),
                    contentType: "application/json",
                    success: function (data) {
                        var json = JSON.parse(data);

                        info.event.setExtendedProp('t_a_id', json.timetable_Activity_Id);
                        info.event.setProp('groupId', json.timetable_Activity_Id);
                        info.event.setExtendedProp('activity_id', activity_id);

                        info.event.setProp("color", json.timetable_Activity_Color);
                        info.event.setProp("textColor", "#fff");
                    },
                    error: function () {
                        swal('Błąd', 'Nie udało się przypisać aktywności. Skontaktuj się z administratorem!', 'error');
                    }
                });
            });
            //
            //alert(info.event.title + " was dropped on A " + info.event.start.toISOString());
        },
        eventDrop: function (info) {
            // moving the element that already exists on the timetable
            // EDIT the day
            swal({
                title: 'Jesteś pewien?',
                text: 'Wprowadzone zmiany będą widoczne dla wszystkich!',
                icon: 'warning',
                buttons: true,
                dangerMode: true
            })
                .then((sure) => {
                    if (sure) {
                        // id of db record in table: timetable_activity
                        var t_a_id = info.event.extendedProps.t_a_id;
                        // update process
                        $.ajax({
                            url: "/Timetable/EditActivityTimeAsync",
                            type: "PUT",
                            data: {
                                id: t_a_id,
                                starttime: new Date(info.event.start.getTime() - (info.event.start.getTimezoneOffset() * 60000)).toISOString(),
                                endtime: new Date(info.event.end.getTime() - (info.event.end.getTimezoneOffset() * 60000)).toISOString(),
                                day: new Date(info.event.start.toISOString()).getDay()
                            },
                            success: function () {
                                swal('Twoje zmiany zostały zapisane!', {
                                    icon: 'success'
                                });
                            },
                            error: function () {
                                info.revert();
                                swal('Coś poszło nie tak!', { icon: 'error' });
                            }
                        });

                    } else {
                        info.revert();
                        swal('Żadne zmiany nie zostały poczynione!');
                    }
                });
        },
        eventResize: function (info) {
            // change size of the event
            // EDIT duration time in db
            swal({
                title: 'Jesteś pewien?',
                text: 'Czy na pewno chcesz zmienić czas trwania aktywności?',
                icon: 'warning',
                buttons: true,
                dangerMode: true
            })
                .then((sure) => {
                    if (sure) {

                        // id of db record in table: timetable_activity
                        var t_a_id = info.event.extendedProps.t_a_id;
                        // update process
                        $.ajax({
                            url: "/Timetable/EditActivityTimeAsync",
                            type: "PUT",
                            data: {
                                id: t_a_id,
                                starttime: new Date(info.event.start.getTime() - (info.event.start.getTimezoneOffset() * 60000)).toISOString(),
                                endtime: new Date(info.event.end.getTime() - (info.event.end.getTimezoneOffset() * 60000)).toISOString(),
                                day: new Date(info.event.start.toISOString()).getDay()
                            },
                            success: function () {
                                swal('Twoje zmiany zostały zapisane!', {
                                    icon: 'success'
                                });
                            },
                            error: function () {
                                info.revert();
                                swal('Coś poszło nie tak!', { icon: 'error' });
                            }
                        });

                    } else {
                        info.revert();
                        swal('Żadne zmiany nie zostały poczynione!', { icon: 'info' });
                    }
                });
        },
        eventClick: function (info) {
            // here we will handle edit action !!
            console.log(info);
            $("#exampleModal").modal({
                show: true
            });



            // pobieramy podstawowe dane
            // ustawiamy wartości w modal oknie
            var timetable_activity_id = info.event.extendedProps.t_a_id;
            var activity_id = info.event.extendedProps.activity_id;
            var limit_places = info.event.extendedProps.t_a_l_p;
            var repeat = info.event.extendedProps.t_a_r;
            var room_id = info.event.extendedProps.room_id;
            var color = info.event.extendedProps.t_a_c;

            $("#ta_limit_places").val(limit_places);
            $("#ta_room_id").val(room_id);
            if (info.event.extendedProps.t_a_r_l === true)
                $("#ta_reservation_list").prop('checked', true);
            else
                $("#ta_reservation_list").prop('checked', false);
            $("#ta_repeatable").val(repeat).trigger('change');

            $("#ta_activity_color").val(info.event.extendedProps.t_a_c);

            //console.log(info.draggedEl.dataset.id);

            $.ajax({
                url: "/Activity/GetActivityEmployees/" + activity_id + "/",
                type: "GET",
                success: function (data) {
                    //console.log(data);
                    $("#ta_employee_id").html("");
                    $.each(data, function (index, value) {
                        $("#ta_employee_id").append($("<option />").val(value.employee_id).text(value.employee_display_name));
                    });
                },
                error: function (data) {
                    alert("Sth went wrong. I am really sorry bro!");
                }
            });

            $(".modal-footer > .btn").unbind('click').click(function (e) {
                e.preventDefault();
                $("#exampleModal").modal('hide');


                var t_activity = createTimetableActivityModel(timetable_activity_id, activity_id, info.event.start, info.event.end, color);

                $.ajax({
                    url: "/Timetable/EditActivityTimeModelAsync/",
                    type: "PUT",
                    data: JSON.stringify(t_activity),
                    contentType: "application/json",
                    success: function (data) {
                        info.event.setProp("color", $("#ta_activity_color").val());
                        swal('Twoje zmiany zostały zapisane!', {
                            icon: 'success'
                        });
                    },
                    error: function () {
                        swal('Błąd', 'Nie udało się zaktualizować aktywności. Skontaktuj się z administratorem!', 'error');
                    }
                });
            });
            //

        },
        eventDragStop: function (info) {
            // DELETING by dropping outside the div.

            var offset = $("#calendar").offset();
            if (info.jsEvent.pageX < offset.left || info.jsEvent.pageX > offset.left + $("#calendar").width() || info.jsEvent.pageY < offset.top || info.jsEvent.pageY > offset.top + $("#calendar").height()) {
                // el is outside the div
                // we can proceed delete
                swal({
                    title: 'Jesteś pewien?',
                    text: 'Czy na pewno chcesz usunąć aktywność z grafiku?',
                    icon: 'warning',
                    buttons: true,
                    dangerMode: true
                })
                    .then((sure) => {
                        if (sure) {
                            info.event.remove();
                            $.ajax({
                                url: "/Timetable/DeleteTimetableActivity",
                                data: {
                                    taId: info.event.extendedProps.t_a_id
                                },
                                type: "DELETE",
                                success: function () {
                                    swal('Twoje zmiany zostały zapisane!', {
                                        icon: 'success'
                                    });
                                },
                                error: function () {
                                    info.revert();
                                    swal('Coś poszło nie tak!', { icon: 'error' });
                                }
                            });
                        }
                        else {
                            swal('Żadne zmiany nie zostały poczynione!', { icon: 'info' });
                        }
                    });
            }
        },
        eventRender: function (info) {
            $(info.el).popover({
                title: "Szczegóły",
                content: "Instruktor: " + info.event.extendedProps.employee_id + "<br /> Pokój: " + info.event.extendedProps.employee_id + "<br /> Limit miejsc: 99 <br />Wolne miejsca: 99",
                trigger: 'hover',
                placement: 'right',
                container: 'body',
                html: true
            });
        }
    });
    calendar.render();

    // trzeba sprawdzić i wrzucić już istniejące 
    // eventy na kalendarz
    $.ajax({
        url: "/Timetable/GetTimetableActivityAsync",
        type: "GET",
        data: { timetable_id: TimetableId },
        success: function (data) {
            $.each(data, function (index, value) {
                // params
                // [0] : nothing
                // [1] : each week
                // [2] : each two weeks
                // [3] : 
                if (value.timetable_Activity_Repeatable === 1) {

                    var start_date = new Date(value.timetable_Activity_Starttime);
                    var end_date = new Date(value.timetable_Activity_Endtime);
                    calendar.addEvent({
                        extendedProps: {
                            t_a_id: value.timetable_Activity_Id,
                            activity_id: value.activity_Id,
                            employee_id: value.employee_Id,
                            t_a_l_p: value.timetable_Activity_Limit_Places,
                            t_a_f_p: value.timetable_Activity_Free_Places,
                            t_a_r: value.timetable_Activity_Repeatable,
                            t_a_c: value.timetable_Activity_Color,
                            t_a_r_l: value.timetable_Activity_Reservation_List,
                            t_a_s: value.timetable_Activity_Status,
                            room_id: value.room_Id
                        },
                        color: value.timetable_Activity_Color,
                        textColor: "#fff",
                        title: value.activity_Title,
                        startTime: (start_date.getHours() < 10 ? '0' : '') + start_date.getHours() + ":" + (start_date.getMinutes() < 10 ? '0' : '') + start_date.getMinutes(),
                        endTime: (end_date.getHours() < 10 ? '0' : '') + end_date.getHours() + ":" + (end_date.getMinutes() < 10 ? '0' : '') + end_date.getMinutes(),
                        daysOfWeek: [start_date.getDay()],
                        start: value.timetable_Activity_Starttime,
                        end: value.timetable_Activity_Endtime
                        
                    });
                }
                else if (value.timetable_Activity_Repeatable === 2) {
                    console.log("I cyk, dwojeczka");
                    var sdate = new Date(value.timetable_Activity_Starttime);
                    var edate = new Date(value.timetable_Activity_Endtime);
                    var start_dates = [];
                    var end_dates = [];
                    var year = sdate.getFullYear() + 1;

                    while (sdate.getFullYear() !== year) {
                        start_dates.push(sdate);
                        end_dates.push(edate);

                        calendar.addEvent({
                            extendedProps: {
                                t_a_id: value.timetable_Activity_Id,
                                activity_id: value.activity_Id,
                                employee_id: value.employee_Id,
                                t_a_l_p: value.timetable_Activity_Limit_Places,
                                t_a_f_p: value.timetable_Activity_Free_Places,
                                t_a_r: value.timetable_Activity_Repeatable,
                                t_a_c: value.timetable_Activity_Color,
                                t_a_r_l: value.timetable_Activity_Reservation_List,
                                t_a_s: value.timetable_Activity_Status,
                                room_id: value.room_Id
                            },
                            color: value.timetable_Activity_Color,
                            textColor: "#fff",
                            title: value.activity_Title,
                            start: sdate,
                            end: edate
                        });

                        sdate = sdate.addDays(14);
                        edate = edate.addDays(14);
                    }
                }
                else
                {
                    calendar.addEvent({
                        extendedProps: {
                            t_a_id: value.timetable_Activity_Id,
                            activity_id: value.activity_Id,
                            employee_id: value.employee_Id,
                            t_a_l_p: value.timetable_Activity_Limit_Places,
                            t_a_f_p: value.timetable_Activity_Free_Places,
                            t_a_r: value.timetable_Activity_Repeatable,
                            t_a_c: value.timetable_Activity_Color,
                            t_a_r_l: value.timetable_Activity_Reservation_List,
                            t_a_s: value.timetable_Activity_Status,
                            room_id: value.room_Id
                        },
                        color: value.timetable_Activity_Color,
                        textColor: "#fff",
                        title: value.activity_Title,
                        start: value.timetable_Activity_Starttime,
                        end: value.timetable_Activity_Endtime,
                    });
                }

            });
        }
    });
    //

    $('#dupa').on('click', function () {
        console.log(calendar.getEvents());
    });
}); 
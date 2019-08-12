function getTimetableId() {
    var pathname = window.location.pathname;
    var link = pathname.split("/");

    return link[link.length - 1];
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
        scrollTime: '09:00:00',
        timeZone: 'UTC',
        plugins: ['interaction', 'timeGrid'],
        defaultView: 'timeGridWeek',
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
                var color = $("#ta_activity_color").val();
                var repeat = $("#ta_repeatable").val();
                var end = new Date(info.event.start.toISOString());

                if (info.event.end === null) 
                    end.setHours(new Date(info.event.start.toISOString()).getHours() + 1);
                else
                    end = info.event.end.toISOString();

                if (color === "#000000")
                    color = original_color;
                
                var timetable_activity = {
                    Employee_Id: $("#ta_employee_id").val(),
                    Room_Id: $("#ta_room_id").val(),
                    Activity_Id: activity_id,
                    Timetable_Activity_Day: new Date(info.event.start.toISOString()).getDay(),
                    Timetable_Activity_Starttime: info.event.start.toISOString(),
                    Timetable_Activity_Endtime: end,
                    Timetable_Activity_Limit_Places: $("#ta_limit_places").val(),
                    Timetable_Activity_Free_Places: $("#ta_limit_places").val(),
                    Timetable_Activity_Repeatable: repeat,
                    Timetable_Activity_Status: "1",
                    Timetable_Activity_Reservation_List: true,
                    Timetable_Activity_Color: color,
                    Timetable_Id: TimetableId
                };


                $.ajax({
                    url: "/Timetable/AddActivityAsync/",
                    type: "POST",
                    data: JSON.stringify(timetable_activity),
                    contentType: "application/json",
                    success: function (data) {
                        var json = JSON.parse(data);
                        info.event.setExtendedProp('t_a_id', json.timetable_Activity_Id);
                        info.event.setProp('groupId', json.timetable_Activity_Id);
                        if (json.timetable_Activity_Color !== "") {
                            info.event.setProp("color", color);
                        }
                        else {
                            info.event.setProp("color", json.timetable_Activity_Color);
                        }
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
                                starttime: info.event.start.toISOString(),
                                endtime: info.event.end.toISOString(),
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
                                starttime: info.event.start.toISOString(),
                                endtime: info.event.end.toISOString(),
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
                // [1] : each week
                // [2] : each two weeks
                if (value.timetable_Activity_Repeatable === 1) {
                    var start_date = new Date(value.timetable_Activity_Starttime);
                    var end_date = new Date(value.timetable_Activity_Endtime);
                    calendar.addEvent({
                        extendedProps: {
                            t_a_id: value.timetable_Activity_Id
                        },
                        color: value.timetable_Activity_Color,
                        textColor: "#fff",
                        title: value.activity_Title,
                        start: value.timetable_Activity_Starttime,
                        end: value.timetable_Activity_Endtime,
                        daysOfWeek: [start_date.getDay()],
                        startTime: start_date.getHours() + ":" + start_date.getMinutes(),
                        endTime: end_date.getHours() + ":" + end_date.getMinutes()
                    });
                }
                else {
                    calendar.addEvent({
                        extendedProps: {
                            t_a_id: value.timetable_Activity_Id
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

document.addEventListener('DOMContentLoaded', function () {
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
        plugins: ['interaction', 'timeGrid'],
        timeZone: 'UTC',
        defaultView: 'timeGridWeek',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek',
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
            // first drop event onto time table
            // here we have to add new record into timetable_activity
            // we will pass whole model to controller
            // new window with some basic details to provide should be shown
            var activity_id = info.draggedEl.dataset.id;
            //console.log(info.draggedEl.dataset.id);
            $.ajax({
                url: "/Activity/GetActivityEmployees/" + activity_id + "/",
                type: "GET",
                success: function (data) {
                    console.log(data);
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

            $(".modal-footer > .btn").on('click', function () {
                $("#exampleModal").modal('hide');
                console.log(info);
                var timetable_activity = {
                    Employee_Id: $("#ta_employee_id").val(),
                    Room_Id: $("#ta_room_id").val(),
                    Activity_Id: activity_id,
                    Timetable_Activity_Day: new Date(info.event.start.toISOString()).getDay(),
                    Timetable_Activity_Starttime: info.event.start.toISOString(),
                    Timetable_Activity_Endtime: info.event.start.toISOString(),
                    Timetable_Activity_Limit_Places: $("#ta_limit_places").val(),
                    Timetable_Activity_Free_Places: $("#ta_limit_places").val(),
                    Timetable_Activity_Repeatable: "1",
                    Timetable_Activity_Status: "1",
                    Timetable_Activity_Reservation_List: true,
                    Timetable_Activity_Color: $("#ta_activity_color").val()
                };

                $.ajax({
                    url: "/Timetable/AddActivityAsync/",
                    type: "POST",
                    data: JSON.stringify(timetable_activity),
                    contentType: "application/json",
                    success: function (data) {
                        var json = JSON.parse(data);
                        info.event.setExtendedProp('t_a_id', json.timetable_Activity_Id);
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
                    alert("OK");
                },
                error: function () {
                    alert("NIE OK");
                }
            });

        },
        eventResize: function (info) {
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
                    alert("OK");
                },
                error: function () {
                    alert("NIE OK");
                }
            });

        }

    });
    calendar.render();

}); 
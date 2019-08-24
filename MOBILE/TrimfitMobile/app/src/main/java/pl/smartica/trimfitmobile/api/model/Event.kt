package pl.smartica.trimfitmobile.model

import java.util.*

class Event {
    var eventId: Int? = null
    var activity: Activity? = null
    var employeeId: Int? = null
    var startTime: Date? = null
    var endTime: Date? = null
}

/*
  {
    "timetable_Activity_Id": 0,
    "employee_Id": 0,
    "activity_Id": 0,
    "timetable_Activity_Day": "string",
    "timetable_Activity_Starttime": "2019-08-21T20:24:36.518Z",
    "timetable_Activity_Endtime": "2019-08-21T20:24:36.518Z",
    "timetable_Activity_Limit_Places": 0,
    "timetable_Activity_Free_Places": 0,
    "room_Id": 0,
    "timetable_Activity_Repeatable": 0,
    "timetable_Activity_Status": 0,
    "timetable_Activity_Reservation_List": true,
    "timetable_Activity_Color": "string",
    "timetable_Id": 0
  }
 */
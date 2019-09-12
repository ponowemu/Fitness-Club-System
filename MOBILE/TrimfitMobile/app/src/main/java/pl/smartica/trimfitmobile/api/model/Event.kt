package pl.smartica.trimfitmobile.api.model

import android.os.Build
import android.util.Log
import org.json.JSONObject
import pl.smartica.trimfitmobile.api.model.Activity
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.util.*

class Event {
    var eventId: Int? = null
    var activity: Activity? = null
    var employee: Employee? = null
    var startTime: LocalDateTime? = null
    var endTime: LocalDateTime? = null
    var color: String? = null

    constructor(item: JSONObject){
        eventId = item.getInt("timetable_Activity_Id")
        activity = Activity(item.getJSONObject("activity"))
        employee = Employee(item.getJSONObject("employee"))
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            startTime = LocalDateTime.parse(item.getString("timetable_Activity_Starttime"),
                DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss"))
            endTime = LocalDateTime.parse(item.getString("timetable_Activity_Endtime"),
                DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss"))
        }
        color = item.getString("timetable_Activity_Color")


    }
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
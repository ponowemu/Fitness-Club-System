package pl.smartica.trimfitmobile.viewModels

import android.content.Context
import android.os.Build
import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.Observer
import androidx.lifecycle.ViewModel
import pl.smartica.trimfitmobile.api.model.Event
import pl.smartica.trimfitmobile.model.DayEvents
import pl.smartica.trimfitmobile.model.SingleDayEvent
import pl.smartica.trimfitmobile.repositories.CalendarRepository
import java.util.*

class CalendarViewModel: ViewModel() {
    private var eventItems= MutableLiveData<MutableList<Event>>()
    private val repository= CalendarRepository()
    private var daysEvents: MutableList<DayEvents> = mutableListOf()

    fun initialize(context: Context){
        eventItems = repository.getItems(context)
    }

    fun getEventList():LiveData<MutableList<Event>>{
        return eventItems
    }

    fun getItemList() : List<DayEvents>{
        setUpItemList()
        daysEvents.sortBy {
            it.monthDay
        }
        return daysEvents
    }

    private fun setUpItemList() {
        for (event in eventItems!!.value!!) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
                var dayText = ""
                when (event.startTime!!.dayOfWeek.toString().toLowerCase()) {
                    "monday" -> dayText = "Mon"
                    "tuesday" -> dayText = "Tue"
                    "wednesday" -> dayText = "Wed"
                    "thursday" -> dayText = "Thu"
                    "friday" -> dayText = "Fri"
                    "saturday" -> dayText = "Sat"
                    "sunday" -> dayText = "Sun"
                }
                Log.v("DAYSSSS: ", dayText)
                var singleEvent = SingleDayEvent(event.activity!!.activityName!!,event.startTime!!,event.endTime!!,event.color!!)
                var day = findDay(event.startTime!!.dayOfMonth.toString())
                if (day == -1) {
                    var itemList: MutableList<SingleDayEvent> = mutableListOf()
                    itemList.add(singleEvent)
                    daysEvents.add(DayEvents(event.startTime!!.dayOfMonth.toString(),dayText,itemList))
                }
                else{
                    daysEvents[day].dayEvents.add(singleEvent)
                }
            }
        }
    }
    private fun findDay(monthDay: String): Int{
        for(i in 0..daysEvents.count() - 1){
            if (daysEvents[i].monthDay == monthDay){
                return i
            }
        }
        return -1
    }
}
package pl.smartica.trimfitmobile.adapters

import android.content.Context
import android.os.Build
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.calendar_item.view.*
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.api.model.Event

enum class Days(var isn: Int){
    Monday(0),

}

class CalendarAdapter(context: Context, itemList: MutableList<Event>): RecyclerView.Adapter<CalendarHolder>() {
    var eventList: MutableList<Event> = mutableListOf()
    var context: Context? = null

    init {
        this.context = context
        this.eventList = itemList
        Log.v("EventAdapter","Constructor")
    }


    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CalendarHolder {
        val view  = LayoutInflater.from(parent.context).inflate(R.layout.calendar_item,parent,false)
        return CalendarHolder(view.calendar_layout)
    }

    override fun getItemCount(): Int {
        return eventList.count()
    }

    override fun onBindViewHolder(holder: CalendarHolder, position: Int) {
        val item = eventList[position]

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
            holder.monthDay.text = item.startTime!!.dayOfMonth.toString()
            var week = item.startTime!!.dayOfWeek.toString()
            if (week.toLowerCase() == "monday")
                holder.weekDay.text =  "Mon"
            if (week.toLowerCase() == "tuesday")
                holder.weekDay.text =  "Tue"
            if (week.toLowerCase() == "wednesday")
                holder.weekDay.text =  "Wed"
            if (week.toLowerCase() == "thursday")
                holder.weekDay.text =  "Thu"
            if (week.toLowerCase() == "friday")
                holder.weekDay.text =  "Fri"
            if (week.toLowerCase() == "saturday")
                holder.weekDay.text =  "Sat"
            if (week.toLowerCase() == "sunday")
                holder.weekDay.text =  "Sun"
        }

    }

}
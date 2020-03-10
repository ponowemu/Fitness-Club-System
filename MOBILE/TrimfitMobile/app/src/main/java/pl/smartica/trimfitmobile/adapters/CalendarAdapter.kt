package pl.smartica.trimfitmobile.adapters

import android.content.Context
import android.os.Build
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.calendar_item.view.*
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.api.model.Event
import pl.smartica.trimfitmobile.model.DayEvents
import pl.smartica.trimfitmobile.model.SingleDayEvent

enum class Days(var isn: Int){
    Monday(0),

}

class CalendarAdapter(context: Context, itemList: List<DayEvents>): RecyclerView.Adapter<CalendarHolder>() {
    var eventList: List<DayEvents> = mutableListOf()
    var context: Context? = null

    init {
        this.context = context
        this.eventList = itemList
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
        holder.monthDay.text = item.monthDay
        holder.weekDay.text = item.weekDay
        var adapter = DayAdapter(
            this.context!!, item.dayEvents
        )
        holder.recyclerView.adapter = adapter
        holder.recyclerView.layoutManager = LinearLayoutManager(this.context)
    }
}
package pl.smartica.trimfitmobile.data.adapters

import android.view.View
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.calendar_item.view.*

class CalendarHolder(itemView: View) : RecyclerView.ViewHolder(itemView)  {
    var weekDay: TextView
    var monthDay: TextView
    var recyclerView: RecyclerView
    init{
        super.itemView
        weekDay = itemView.week_day
        monthDay = itemView.month_day
        recyclerView = itemView.day_recyclerView
    }
}
package pl.smartica.trimfitmobile.adapters

import android.content.Context
import android.os.Build
import android.text.Html
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.day_item.view.*
import kotlinx.android.synthetic.main.service_item.view.*
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.api.model.Service
import pl.smartica.trimfitmobile.api.model.SingleDayEvent

class DayAdapter(context: Context, itemList: MutableList<SingleDayEvent>): RecyclerView.Adapter<DayHolder> {
    var eventDayList: MutableList<SingleDayEvent> = mutableListOf()
    var context: Context? = null

    init {
        this.context = context
        this.eventDayList = itemList
    }
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): DayHolder {
        val view  = LayoutInflater.from(parent.context).inflate(R.layout.day_item,parent,false)
        return DayHolder(view.day_layout)
    }

    override fun getItemCount(): Int {
        return eventDayList.count()
    }

    override fun onBindViewHolder(holder: DayHolder, position: Int) {
        var item = eventDayList[position]
        holder.
    }
}

}

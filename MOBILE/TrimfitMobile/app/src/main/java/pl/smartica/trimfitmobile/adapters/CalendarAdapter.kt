package pl.smartica.trimfitmobile.data.adapters

import android.content.Context
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.calendar_item.view.*
import kotlinx.android.synthetic.main.service_item.view.*
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.model.Event
import pl.smartica.trimfitmobile.model.Service

class CalendarAdapter(context: Context, itemList: MutableList<Event>): RecyclerView.Adapter<ServiceHolder>() {
    var eventList: MutableList<Event> = mutableListOf()
    var context: Context? = null

    init {
        this.context = context
        this.eventList = itemList
        Log.v("ServiceAdapter","Constructor")
    }


    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ServiceHolder {
        val view  = LayoutInflater.from(parent.context).inflate(R.layout.calendar_item,parent,false)
        return ServiceHolder(view.calendar_layout)
    }

    override fun getItemCount(): Int {
        return eventList.count()
    }

    override fun onBindViewHolder(holder: ServiceHolder, position: Int) {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

}
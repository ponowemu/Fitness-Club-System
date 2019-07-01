package pl.smartica.trimfitmobile

import android.view.ViewGroup
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView
import android.content.Context
import android.os.Build
import android.text.Html
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import pl.smartica.trimfitmobile.model.Service
import kotlinx.android.synthetic.main.service_item.view.*
import kotlinx.android.synthetic.main.service_item.*

class ServiceAdapter(context: Context, itemList: MutableList<Service>): RecyclerView.Adapter<ServiceHolder>() {
    var serviceList: MutableList<Service> = mutableListOf()
    var context: Context? = null

    init {
        this.context = context
        this.serviceList = itemList
        Log.v("ServiceAdapter","Constructor")
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ServiceHolder {
        val view  = LayoutInflater.from(parent.context).inflate(R.layout.service_item,parent,false)
        return ServiceHolder(view.service_layout)
    }

    override fun getItemCount(): Int {
        return serviceList.count()
    }

    override fun onBindViewHolder(holder: ServiceHolder, position: Int) {

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            holder.descriptionText!!.text = Html.fromHtml(serviceList[position].serviceDescription, Html.FROM_HTML_MODE_COMPACT)
            holder.headerText!!.text = Html.fromHtml(serviceList[position].serviceHeader, Html.FROM_HTML_MODE_COMPACT)
        } else {
            holder.descriptionText!!.text = Html.fromHtml(serviceList[position].serviceDescription)
            holder.headerText!!.text = Html.fromHtml(serviceList[position].serviceHeader)
        }

    }


}

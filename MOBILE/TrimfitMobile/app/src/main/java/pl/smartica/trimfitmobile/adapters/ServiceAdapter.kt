package pl.smartica.trimfitmobile.adapters

import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import android.content.Context
import android.os.Build
import android.text.Html
import android.util.Log
import android.view.LayoutInflater
import kotlinx.android.synthetic.main.service_item.view.*
import pl.smartica.trimfitmobile.R
import pl.smartica.trimfitmobile.api.model.Service

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
        holder.priceText.text = serviceList[position].servicePriceGross.toString()
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            holder.descriptionText.text = Html.fromHtml(serviceList[position].serviceDescription, Html.FROM_HTML_MODE_COMPACT)
            holder.headerText.text = Html.fromHtml(serviceList[position].serviceHeader, Html.FROM_HTML_MODE_COMPACT)
        } else {
            holder.descriptionText.text = Html.fromHtml(serviceList[position].serviceDescription)
            holder.headerText.text = Html.fromHtml(serviceList[position].serviceHeader)
        }

    }
}

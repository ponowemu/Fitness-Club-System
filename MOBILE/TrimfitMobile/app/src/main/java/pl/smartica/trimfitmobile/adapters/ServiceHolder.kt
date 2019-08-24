package pl.smartica.trimfitmobile.data.adapters

import android.util.Log
import android.view.View
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.service_item.view.*

class ServiceHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    var headerText: TextView
    var descriptionText: TextView
    var priceText: TextView
    var parentLayout:ConstraintLayout

    init{
        super.itemView
        headerText = itemView.service_header
        descriptionText = itemView.service_description
        parentLayout = itemView.service_layout
        priceText = itemView.price_gross
    }

}
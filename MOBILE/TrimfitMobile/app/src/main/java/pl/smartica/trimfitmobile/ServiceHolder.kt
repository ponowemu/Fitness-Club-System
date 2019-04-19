package pl.smartica.trimfitmobile

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.service_item.view.*
import kotlinx.android.synthetic.main.service_item.service_header

class ServiceHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    var headerText: TextView? = null
    var descriptionText: TextView? = null
    var parentLayout:ConstraintLayout? = null

    init{
         super.itemView
         headerText = itemView.service_header
         descriptionText = itemView.service_description
         parentLayout = itemView.service_layout
    }

}
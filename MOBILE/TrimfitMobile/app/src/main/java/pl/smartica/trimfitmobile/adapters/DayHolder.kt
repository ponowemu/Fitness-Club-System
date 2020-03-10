package pl.smartica.trimfitmobile.adapters

import android.view.View
import android.widget.TextView
import androidx.constraintlayout.widget.ConstraintLayout
import androidx.recyclerview.widget.RecyclerView
import kotlinx.android.synthetic.main.day_item.view.*

class DayHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
    var nameText: TextView
    var hourRangeText: TextView
    var parentLayout: ConstraintLayout

    init{
        super.itemView
        nameText = itemView.activity_name_textView
        hourRangeText = itemView.hour_range_textView
        parentLayout = itemView.day_layout
    }
}
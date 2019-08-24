package pl.smartica.trimfitmobile

import android.content.Context
import android.graphics.Typeface
import android.util.AttributeSet
import android.view.Gravity
import android.widget.TextView


class IconTextView : TextView {

    constructor(context: Context) : super(context) {
        createView()
    }

    constructor(context: Context, attrs: AttributeSet) : super(context, attrs) {
        createView()
    }

    private fun createView() {
        gravity = Gravity.CENTER
        typeface = Typeface.createFromAsset( context.assets, "icons.ttf" )
    }
}
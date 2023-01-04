package com.example.monitumpdm

import android.content.Context
import android.graphics.Canvas
import android.graphics.Color
import android.graphics.Paint
import android.graphics.Rect
import android.graphics.drawable.Drawable
import android.util.AttributeSet
import android.view.View
import android.widget.TextView
import org.w3c.dom.Text

class SalaStatusColor: View {
    constructor(context: Context?) : super(context)
    constructor(context: Context?, attrs: AttributeSet?) : super(context, attrs)
    constructor(context: Context?, attrs: AttributeSet?, defStyleAttr: Int) : super(
        context,
        attrs,
        defStyleAttr
    )

    var isOpen = false

    override fun onDraw(canvas: Canvas?){
        super.onDraw(canvas)

        val paint = Paint()
        paint.color = (Color.parseColor("#F53333"))

        val id = this.id
        if (isOpen){
            paint.color = (Color.parseColor("#3EE723"))
        } else {
            paint.color = (Color.parseColor("#F53333"))
        }
        canvas?.drawRect(Rect(0, 0, width, height), paint)
    }
}
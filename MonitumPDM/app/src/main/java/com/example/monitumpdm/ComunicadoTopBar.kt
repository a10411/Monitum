package com.example.monitumpdm

import android.content.Context
import android.graphics.Canvas
import android.graphics.Color
import android.graphics.Paint
import android.graphics.Rect
import android.util.AttributeSet
import android.view.View

class ComunicadoTopBar : View {
    constructor(context: Context?) : super(context)
    constructor(context: Context?, attrs: AttributeSet?) : super(context, attrs)
    constructor(context: Context?, attrs: AttributeSet?, defStyleAttr: Int) : super(
        context,
        attrs,
        defStyleAttr
    )

    override fun onDraw(canvas: Canvas?){
        super.onDraw(canvas)

        val paint = Paint()
        paint.color = (Color.parseColor("#3C60FF"))
        canvas?.drawRect(Rect(0, 0, width, height), paint)
    }
}
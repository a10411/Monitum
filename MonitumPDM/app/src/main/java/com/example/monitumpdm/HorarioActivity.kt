package com.example.monitumpdm

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import android.widget.ListView
import android.widget.TextView
import androidx.lifecycle.lifecycleScope

class HorarioActivity : AppCompatActivity() {

    var horarios = arrayListOf<Horario>()

    val adapter = HorariosAdapter()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_horario)

        val idSala = intent.getIntExtra("idSala", 0)
        val listViewHorarios = findViewById<ListView>(R.id.listViewHorario)
        listViewHorarios.adapter = adapter

        HorarioRequests.getHorarios(idSala,lifecycleScope){
            horarios = it
            adapter.notifyDataSetChanged()
        }



    }

    inner class HorariosAdapter : BaseAdapter() {
        override fun getCount(): Int {
            return horarios.size
        }

        override fun getItem(pos: Int): Any {
            return horarios[pos]
        }

        override fun getItemId(p0: Int): Long {
            return 0L
        }

        override fun getView(pos: Int, view: View?, parent: ViewGroup?): View {
            val rootView = layoutInflater.inflate(R.layout.row_horario, parent, false)

            val textViewDia = rootView.findViewById<TextView>(R.id.textViewDia)
            val textViewHora = rootView.findViewById<TextView>(R.id.textViewHora)

            textViewDia.text = horarios[pos].diaSemana
            textViewHora.text = "${horarios[pos].horaEntrada} às ${horarios[pos].horaSaida}"

            return rootView

        }
    }
}

package com.example.monitumpdm

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import android.widget.ListView
import android.widget.TextView

class GestorVerComunicadosActivity : AppCompatActivity() {
    // SO ENTRA NESTA ACTIVITY CASO SEJA GESTOR!
    // SO ENTRA NESTA ACTIVITY CASO SEJA GESTOR!
    // SO ENTRA NESTA ACTIVITY CASO SEJA GESTOR!

    var comunicados = arrayListOf<Comunicado>()

    val adapter = ComunicadosAdapter()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_ver_comunicados)

        val listViewComunicados = findViewById<ListView>(R.id.listViewComunicadosGestorVerComunicados)
        listViewComunicados.adapter = adapter

        // request
        comunicados.add(Comunicado(0, 0, "Sou um comunicado", "Corpo comunicado", null))
        comunicados.add(Comunicado(0, 0, "Sou um comunicado", "Corpo comunicado", null))
        comunicados.add(Comunicado(0, 0, "Sou um comunicado", "Corpo comunicado", null))
        comunicados.add(Comunicado(0, 0, "Sou um comunicado", "Corpo comunicado", null))
        adapter.notifyDataSetChanged()
    }

    inner class ComunicadosAdapter: BaseAdapter(){
        override fun getCount(): Int {
            return comunicados.size
        }

        override fun getItem(pos: Int): Any {
            return  comunicados[pos]
        }

        override fun getItemId(p0: Int): Long {
            return 0L
        }

        override fun getView(pos: Int, view: View?, parent: ViewGroup?): View {
            val rootView = layoutInflater.inflate(R.layout.row_comunicado, parent, false)

            // textViews
            //val textViewNomeSala = rootView.findViewById<TextView>(R.id.textViewNomeSala)
            //val textViewEstadoSala = rootView.findViewById<TextView>(R.id.textViewEstadoSala)

            //textViewNomeSala.text = salas[pos].nome
            //textViewEstadoSala.text = salas[pos].Estado



            return rootView

        }

    }
}
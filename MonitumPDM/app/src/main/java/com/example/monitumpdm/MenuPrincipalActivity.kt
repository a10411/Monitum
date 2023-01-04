package com.example.monitumpdm

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import android.widget.ListView
import android.widget.TextView
import androidx.lifecycle.lifecycleScope

class MenuPrincipalActivity : AppCompatActivity() {
    var salas = arrayListOf<Sala>()

    val adapter = SalasAdapter()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_menu_principal)

        val listViewSalas = findViewById<ListView>(R.id.listViewSalas)
        listViewSalas.adapter = adapter

        SalaRequests.getAllSalas(lifecycleScope){
            salas = it
            adapter.notifyDataSetChanged()
        }
    }

    inner class SalasAdapter: BaseAdapter(){
        override fun getCount(): Int {
            return salas.size
        }

        override fun getItem(pos: Int): Any {
            return  salas[pos]
        }

        override fun getItemId(p0: Int): Long {
            return 0L
        }

        override fun getView(pos: Int, view: View?, parent: ViewGroup?): View {
            val rootView = layoutInflater.inflate(R.layout.row_sala, parent, false)

            val textViewNomeSala = rootView.findViewById<TextView>(R.id.textViewNomeSala)
            val textViewEstadoSala = rootView.findViewById<TextView>(R.id.textViewEstadoSala)

            textViewNomeSala.text = salas[pos].nome;
            textViewEstadoSala.text = salas[pos].Estado; //trocar! ir buscar se esta aberta ou fechada


            return rootView

        }

    }
}
package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.ViewGroup
import android.widget.BaseAdapter
import android.widget.Button
import android.widget.ListView
import android.widget.TextView
import androidx.lifecycle.lifecycleScope
import org.w3c.dom.Text

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

        val idSala = intent.getIntExtra("idSala", 0)
        var nomeSala = ""

        SalaRequests.getSalaByIdSala(lifecycleScope, idSala){
            nomeSala = it.nome!!
            for (comunicado in comunicados){
                comunicado.nomeSala = nomeSala
            }
        }


        ComunicadoRequests.getComunicadosByIdSala(lifecycleScope, idSala){
            for (comunicado in it){
                comunicado.nomeSala = nomeSala
            }
            comunicados = it
            comunicados.sortByDescending { Comunicado -> Comunicado.dataHora }
            adapter.notifyDataSetChanged()
        }

        findViewById<Button>(R.id.buttonAdicionarComunicado).setOnClickListener{
            val intent = Intent(this@GestorVerComunicadosActivity, GestorAdicionarComunicadoActivity::class.java)
            intent.putExtra("idSala", idSala)
            startActivity(intent)
        }


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
            val textViewSalaComunicado = rootView.findViewById<TextView>(R.id.textViewSalaComunicado)
            val textViewDataComunicado = rootView.findViewById<TextView>(R.id.textViewDataComunicado)
            val textViewTituloComunicado = rootView.findViewById<TextView>(R.id.textViewTituloComunicado)
            val textViewCorpoComunicado = rootView.findViewById<TextView>(R.id.textViewCorpoComunicado)

            textViewSalaComunicado.text = comunicados[pos].nomeSala // passar para nome
            textViewDataComunicado.text = comunicados[pos].dataHora.toString()
            textViewTituloComunicado.text = comunicados[pos].titulo
            textViewCorpoComunicado.text = comunicados[pos].corpo

            return rootView

        }

    }
}
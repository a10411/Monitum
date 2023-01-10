package com.example.monitumpdm

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.lifecycle.lifecycleScope

class MenuPrincipalGestorActivity : AppCompatActivity() {
    var salas = arrayListOf<Sala>()
    val adapter = SalasAdapter()



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_menu_principal_gestor)

        val listViewSalas = findViewById<ListView>(R.id.listViewSalasGestor)
        listViewSalas.adapter = adapter

        SalaRequests.getAllSalas(lifecycleScope){
            salas = it
            adapter.notifyDataSetChanged()
        }


    }

    inner class SalasAdapter: BaseAdapter() {
        override fun getCount(): Int {
            return salas.size
        }

        override fun getItem(pos: Int): Any {
            return salas[pos]
        }

        override fun getItemId(pos: Int): Long {
            return 0L
        }

        override fun getView(pos: Int, view: View?, parent: ViewGroup?): View {
            val rootView = layoutInflater.inflate(R.layout.row_sala, parent, false)

            val textViewNomeSala = rootView.findViewById<TextView>(R.id.textViewNomeSala)
            val textViewEstadoSala = rootView.findViewById<TextView>(R.id.textViewEstadoSala)

            textViewNomeSala.text = salas[pos].nome
            textViewEstadoSala.text = salas[pos].Estado

            val salaStatusColor = rootView.findViewById<SalaStatusColor>(R.id.salaStatusColor)
            if(salas[pos].Estado == "Aberta"){
                salaStatusColor.color = "#3EE723"
            }else{
                salaStatusColor.color = "#F53333"
            }

            rootView.setOnClickListener{
                val intent = Intent(this@MenuPrincipalGestorActivity, SalaDetailGestorActivity::class.java)
                intent.putExtra("idSala", salas[pos].idSala)
                intent.putExtra("nome", salas[pos].nome)
                intent.putExtra("idEstabelecimento", salas[pos].idEstabelecimento)
                intent.putExtra("idEstado", salas[pos].idEstado)
                intent.putExtra("Estado", salas[pos].Estado)
                intent.putExtra("token", intent.getStringExtra("token"))
                startActivity(intent)
            }

            val buttonAddSala: Button = findViewById(R.id.buttonAddSala)
                buttonAddSala.setOnClickListener{
                    val intent = Intent(this@MenuPrincipalGestorActivity, GestorAdicionarSalaActivity::class.java)
                    startActivity(intent)
                }



            return rootView
        }
    }
}
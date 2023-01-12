package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.*
import androidx.lifecycle.lifecycleScope

class GestorEditarSalaActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_editar_sala)

        var sala = Sala()
        sala.idSala = intent.getIntExtra("idSala", 0)
        sala.nome = intent.getStringExtra("nome")
        sala.Estado = intent.getStringExtra("Estado")
        sala.idEstado = intent.getIntExtra("idEstado", 0)

        val editTextNome = findViewById<EditText>(R.id.editTextNomeEditarSala)

        var estados = arrayListOf<Estado>()
        var estadosNomes = arrayListOf<String>()
        val preferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE)
        val sessionToken = preferences.getString("session_token", null)

        EstadoRequests.getAllEstados(lifecycleScope){
            estados = it
            for (estado in estados){
                estadosNomes.add(estado.estado!!)
            }
            val spinner: Spinner = findViewById(R.id.spinnerEstado)
            val adapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, estadosNomes)
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_item)
            spinner.adapter = adapter


            spinner.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
                override fun onItemSelected(parent: AdapterView<*>, view: View, position: Int, id: Long) {
                    val selectedItem = estadosNomes[position]
                    Log.d("Spinner", "Selected:$selectedItem")
                }
                override fun onNothingSelected(position: AdapterView<*>?) {

                }
            }
        }


        findViewById<Button>(R.id.buttonCancelarSala).setOnClickListener{
            val intent = Intent(this@GestorEditarSalaActivity, MenuPrincipalGestorActivity::class.java)
            startActivity(intent)
        }


    }
}
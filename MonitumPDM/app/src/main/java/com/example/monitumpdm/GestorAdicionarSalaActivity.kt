package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.*
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.lifecycleScope


class GestorAdicionarSalaActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {


        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_adicionar_sala)

        var estados = arrayListOf<Estado>()
        var estadosNomes = arrayListOf<String>()
        val preferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE)
        val sessionToken = preferences.getString("session_token", null)

        EstadoRequests.getAllEstados(lifecycleScope){
            estados = it
            for (estado in estados){
                estadosNomes.add(estado.estado!!)
            }
            val spinner: Spinner = findViewById(R.id.spinnerEstadoAdicionarSala)
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
            val intent = Intent(this@GestorAdicionarSalaActivity, MenuPrincipalGestorActivity::class.java)
            startActivity(intent)
        }



        findViewById<Button>(R.id.buttonConfirmarAdicionarSala).setOnClickListener{
            val sala = Sala()
            sala.nome = findViewById<EditText>(R.id.editTextNomeEditarSala).text.toString()
            sala.Estado = findViewById<Spinner>(R.id.spinnerEstadoAdicionarSala).selectedItem.toString()

            for (estado in estados){
                if (sala.Estado == estado.estado){
                    sala.idEstado = estado.idEstado
                }
            }


            SalaRequests.addSala(lifecycleScope, sala){response ->
                if(response == "Error adding sala"){
                    Toast(this).showCustomToast("Erro ao adicionar sala", this)
                    //Toast.makeText(this, "Erro ao adicionar sala", Toast.LENGTH_LONG).show()
                }else{
                    Toast(this).showCustomToast("Sala adicionada!", this)
                    var salas2 = arrayListOf<Sala>()
                    SalaRequests.getAllSalas(lifecycleScope){
                        salas2 = it
                        val lastSala = salas2.last().idSala!!
                        SalaRequests.addLogMetrica(lifecycleScope ,lastSala, sessionToken){ response2->

                            if(response2 == "Error adding sala"){
                                Toast(this).showCustomToast("Erro ao adicionar log", this)
                                //Toast.makeText(this, "Erro ao adicionar log", Toast.LENGTH_LONG).show()
                            }else{
                                Toast(this).showCustomToast("Log adicionada!", this)
                                //Toast.makeText(this, "Log adicionada!", Toast.LENGTH_LONG).show()
                                startActivity(Intent(this@GestorAdicionarSalaActivity, MenuPrincipalGestorActivity::class.java))

                            }
                        }
                    }


                    //Toast.makeText(this, "Sala adicionada!", Toast.LENGTH_LONG).show()
                    startActivity(Intent(this@GestorAdicionarSalaActivity, MenuPrincipalGestorActivity::class.java))

                }
            }





        }
    }
}
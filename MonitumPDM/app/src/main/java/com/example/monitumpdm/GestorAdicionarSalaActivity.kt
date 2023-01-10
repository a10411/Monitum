package com.example.monitumpdm

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.*
import androidx.lifecycle.lifecycleScope

class GestorAdicionarSalaActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {


        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_adicionar_sala)

        val spinner: Spinner = findViewById(R.id.spinnerEstadoAdicionarSala)

        val adapter = ArrayAdapter.createFromResource(this,
            R.array.estados_array, android.R.layout.simple_spinner_item)
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_item)
        spinner.adapter = adapter
        
        spinner.onItemSelectedListener = object :
        AdapterView.OnItemSelectedListener{
            override fun onItemSelected(parent: AdapterView<*>, view: View, position: Int, id: Long) {
                val selectedItem = parent.getItemAtPosition(position)
                Log.d("Spinner", "Selected:$selectedItem")
            }

            override fun onNothingSelected(p0: AdapterView<*>?) {

            }
        }

        findViewById<Button>(R.id.buttonCancelarSala).setOnClickListener{
            val intent = Intent(this@GestorAdicionarSalaActivity, MenuPrincipalGestorActivity::class.java)
            startActivity(intent)
        }

        findViewById<Button>(R.id.buttonCriarSala).setOnClickListener{
            val sala = Sala()
            sala.nome = findViewById<EditText>(R.id.editTextNomeEditarSala).text.toString()
            sala.Estado = findViewById<Spinner>(R.id.spinnerEstadoAdicionarSala).selectedItem.toString()
            if (sala.Estado == "Ativa")
            {
                sala.idEstado = 1
            }else if(sala.Estado == "Inativa"){
                sala.idEstado == 2
            }

            SalaRequests.addSala(lifecycleScope, sala){result ->
                if(result == "Error adding sala"){
                    Toast.makeText(this, "Erro ao adicionar sala", Toast.LENGTH_LONG).show()
                }else{
                    Toast.makeText(this, "Sala adicionada!", Toast.LENGTH_LONG).show()

                    startActivity(Intent(this@GestorAdicionarSalaActivity, MenuPrincipalGestorActivity::class.java))
                }
            }
        }
    }
}
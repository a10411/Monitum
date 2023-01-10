package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.lifecycle.lifecycleScope

class GestorAdicionarComunicadoActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_adicionar_comunicado)

        val preferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE)
        val sessionToken = preferences.getString("session_token", null)
        val idSala = intent.getIntExtra("idSala", 0)
        findViewById<Button>(R.id.buttonCancelarComunicado).setOnClickListener{
            val intent = Intent(this@GestorAdicionarComunicadoActivity, GestorVerComunicadosActivity::class.java)
            startActivity(intent)
        }

        findViewById<Button>(R.id.buttonAdicionarComunicado2).setOnClickListener {
            val comunicado = Comunicado()
            comunicado.idSala = idSala
            comunicado.titulo = findViewById<EditText>(R.id.editTextTituloComunicado).text.toString()
            comunicado.corpo = findViewById<EditText>(R.id.editTextCorpoComunicado).text.toString()
            ComunicadoRequests.addComunicado(lifecycleScope, comunicado, sessionToken!!){ result ->
                if(result == "Error adding comunicado"){
                    Toast.makeText(this,"Erro ao adicionar comunicado", Toast.LENGTH_LONG).show()
                }else{
                    Toast.makeText(this, "Comunicado adicionado!", Toast.LENGTH_LONG).show()
                    startActivity(Intent(this@GestorAdicionarComunicadoActivity, GestorVerComunicadosActivity::class.java))
                }

            }
        }
    }
}
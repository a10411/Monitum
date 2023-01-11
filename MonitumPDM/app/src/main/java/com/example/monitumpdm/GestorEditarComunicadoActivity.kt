package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.lifecycle.lifecycleScope
import kotlin.coroutines.coroutineContext

class GestorEditarComunicadoActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_editar_comunicado)

        var comunicado = Comunicado()
        comunicado.idComunicado = intent.getIntExtra("idComunicado", 0)
        // Toast.makeText(this, comunicado.idComunicado.toString(), Toast.LENGTH_LONG).show()
        comunicado.titulo = intent.getStringExtra("titulo")
        comunicado.corpo = intent.getStringExtra("corpo")

        val editTextTitulo = findViewById<EditText>(R.id.editTextTituloComunicadoEdit)
        val editTextCorpo = findViewById<EditText>(R.id.editTextCorpoComunicadoEdit)
        editTextTitulo.setText(comunicado.titulo)
        editTextCorpo.setText(comunicado.corpo)

        val preferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE)
        val sessionToken = preferences.getString("session_token", null)

        findViewById<Button>(R.id.buttonCancelarEditComunicado).setOnClickListener{
            val intent = Intent(this@GestorEditarComunicadoActivity, GestorVerComunicadosActivity::class.java)
            startActivity(intent)
        }
        findViewById<Button>(R.id.buttonApagarComunicado).setOnClickListener {
            val builder = AlertDialog.Builder(this@GestorEditarComunicadoActivity)
            builder.setMessage("Tem a certeza que quer apagar?")
                .setCancelable(false)
                .setPositiveButton("Sim") { dialog, id ->
                    ComunicadoRequests.deleteComunicado(lifecycleScope, comunicado.idComunicado!!,
                        sessionToken!!
                    ){ result ->
                        if (result == "Sucesso"){
                            Toast.makeText(this,"Comunicado apagado!", Toast.LENGTH_LONG).show()
                        } else {
                            Toast.makeText(this,"Erro!", Toast.LENGTH_LONG).show()
                        }
                        val intent = Intent(this@GestorEditarComunicadoActivity, GestorVerComunicadosActivity::class.java)
                        startActivity(intent)
                    }
                }
                .setNegativeButton("Não") { dialog, id ->
                    // Dismiss the dialog
                    dialog.dismiss()
                }
            val alert = builder.create()
            alert.show()
        }
        findViewById<Button>(R.id.buttonEditarComunicadoDetail).setOnClickListener {
            var comunicado2 = comunicado
            comunicado2.titulo = editTextTitulo.text.toString()
            comunicado2.corpo = editTextCorpo.text.toString()
            val builder = AlertDialog.Builder(this@GestorEditarComunicadoActivity)
            builder.setMessage("Tem a certeza que quer editar?")
                .setCancelable(false)
                .setPositiveButton("Sim") { dialog, id ->
                    ComunicadoRequests.updateComunicado(lifecycleScope, comunicado2,
                        sessionToken!!
                    ){ result ->
                        if (result == "Sucesso"){
                            Toast.makeText(this,"Comunicado atualizado!", Toast.LENGTH_LONG).show()
                        } else {
                            Toast.makeText(this,"Erro!", Toast.LENGTH_LONG).show()
                        }
                        val intent = Intent(this@GestorEditarComunicadoActivity, GestorVerComunicadosActivity::class.java)
                        startActivity(intent)
                    }
                }
                .setNegativeButton("Não") { dialog, id ->
                    // Dismiss the dialog
                    dialog.dismiss()
                }
            val alert = builder.create()
            alert.show()



        }
    }
}
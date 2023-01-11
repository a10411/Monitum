package com.example.monitumpdm

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AlertDialog

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

        findViewById<Button>(R.id.buttonCancelarEditComunicado).setOnClickListener{
            val intent = Intent(this@GestorEditarComunicadoActivity, GestorVerComunicadosActivity::class.java)
            startActivity(intent)
        }
        findViewById<Button>(R.id.buttonApagarComunicado).setOnClickListener {
            val builder = AlertDialog.Builder(this@GestorEditarComunicadoActivity)
            builder.setMessage("Tem a certeza que quer apagar?")
                .setCancelable(false)
                .setPositiveButton("Sim") { dialog, id ->
                    // Request apagar TODO

                }
                .setNegativeButton("Não") { dialog, id ->
                    // Dismiss the dialog
                    dialog.dismiss()
                }
            val alert = builder.create()
            alert.show()
        }
        findViewById<Button>(R.id.buttonEditarComunicadoDetail).setOnClickListener {
            val builder = AlertDialog.Builder(this@GestorEditarComunicadoActivity)
            builder.setMessage("Tem a certeza que quer editar?")
                .setCancelable(false)
                .setPositiveButton("Sim") { dialog, id ->
                    // Request editar TODO

                    // voltar
                    val intent = Intent(this@GestorEditarComunicadoActivity, GestorVerComunicadosActivity::class.java)
                    startActivity(intent)
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
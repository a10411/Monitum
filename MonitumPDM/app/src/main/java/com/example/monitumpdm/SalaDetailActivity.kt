package com.example.monitumpdm

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView

class SalaDetailActivity : AppCompatActivity() {

    val Sala = Sala(null, null, null, null, null)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sala_detail)

        val idSala = intent.getStringExtra("idSala")
        val nome = intent.getStringExtra("nome")
        val idEstabelecimento = intent.getIntExtra("idEstabelecimento", 0)
        val idEstado = intent.getIntExtra("idEstado", 0)
        val estado = intent.getStringExtra("Estado")

        findViewById<TextView>(R.id.textViewNomeSalaSalaDetail).text = nome

    }
}
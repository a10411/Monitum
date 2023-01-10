package com.example.monitumpdm

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle

class GestorEditarSalaActivity : AppCompatActivity() {



    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_gestor_editar_sala)


        val idSala = intent.getIntExtra("idSala", 0)
        val nome = intent.getStringExtra("nome")
        val estado = intent.getIntExtra("Estado", 0)


    }
}
package com.example.monitumpdm

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import androidx.lifecycle.lifecycleScope

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        var salas = arrayListOf<Sala>()
        SalaRequests.getAllSalas(lifecycleScope){
            salas = it
            // Log.d("MainActivity", salas.toString())
        }


    }
}
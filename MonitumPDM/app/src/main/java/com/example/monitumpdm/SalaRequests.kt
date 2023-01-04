package com.example.monitumpdm

import android.util.Log
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import org.json.JSONObject
import java.io.IOException

object SalaRequests {
    private val client = OkHttpClient()

    fun getAllSalas(scope: CoroutineScope, callback: (ArrayList<Sala>)->Unit){
        scope.launch(Dispatchers.IO){
            val request = Request.Builder().url("https://6d6a-2001-8a0-fe0e-4b00-d869-5f4d-2f4b-d1f6.eu.ngrok.io/estabelecimento/1").get().build()
            // meter em utils o link da api

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"){
                        var salas = arrayListOf<Sala>()
                        val salaJSONData = jsonObject.getJSONArray("data")
                        // Log.d("SalaRequests", salaJSONData.toString())
                        for (i in 0 until salaJSONData.length()){
                            val item = salaJSONData.getJSONObject(i)
                            val sala = Sala.fromJSON(item)
                            sala.Estado = checkSalaOpen(scope, sala.idSala!!)
                            salas.add(sala)
                        }
                        scope.launch(Dispatchers.Main){
                            callback(salas)
                        }
                    }

            }
        }
    }

    fun checkSalaOpen(scope: CoroutineScope, idSala: Int ): String{
        val request = Request.Builder().url("https://6d6a-2001-8a0-fe0e-4b00-d869-5f4d-2f4b-d1f6.eu.ngrok.io/CheckSalaOpen/sala/$idSala").get().build()

        client.newCall(request).execute().use{ response ->
            if(!response.isSuccessful) throw IOException("Unexpected code $response")

            val result = response.body!!.string()

            val jsonObject = JSONObject(result)
            if (jsonObject.getString("statusCode") == "200"){
                val salaJSONData = jsonObject.getBoolean("data")
                if (salaJSONData == false){
                    return "Fechada"
                } else {
                    return "Aberta"
                }
                return salaJSONData.toString()
            } else {
                return "Indefinido"
            }

        }
    }
}
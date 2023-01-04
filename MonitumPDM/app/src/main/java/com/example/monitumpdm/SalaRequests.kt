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
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/estabelecimento/1").get().build()
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
                            sala.Estado = checkSalaOpen(sala.idSala!!)
                            if (sala.idEstado == 1){
                                salas.add(sala) // nao mostrar inativas
                            }
                        }
                        scope.launch(Dispatchers.Main){
                            callback(salas)
                        }
                    }

            }
        }
    }

    fun checkSalaOpen(idSala: Int ): String{
        val link = UtilsAPI().connectionNgRok()
        val request = Request.Builder().url("${link}/CheckSalaOpen/sala/$idSala").get().build()

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
            } else {
                return "Indefinido"
            }

        }
    }
}
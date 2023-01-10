package com.example.monitumpdm

import android.util.Log
import android.widget.Toast
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody.Companion.toRequestBody
import org.json.JSONObject
import java.io.IOException

object SalaRequests {
    private val client = OkHttpClient()

    fun getAllSalas(scope: CoroutineScope, callback: (ArrayList<Sala>)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/estabelecimento/1").get().build()

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

    fun checkSalaOpen(idSala: Int): String{
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


    fun getSalaByIdSala(scope: CoroutineScope, idSala: Int, callback: (Sala)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/GetSalaByIdSala/${idSala}").get().build()

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"){
                    var sala = Sala()
                    val salaJSONData = jsonObject.getJSONObject("data")
                    sala.idSala = salaJSONData.getInt("idSala")
                    sala.nome = salaJSONData.getString("nome")
                    sala.idEstabelecimento = salaJSONData.getInt("idEstabelecimento")
                    sala.idEstado = salaJSONData.getInt("idEstado")
                    scope.launch(Dispatchers.Main){
                        callback(sala)
                    }
                }

            }
        }
    }
    
    fun editSala(sala: Sala): String{
        val link = UtilsAPI().connectionNgRok()
        val jsonBody = """
            {
                "nome": "${sala.nome}",
                "idEstabelecimento": 0,
                "idEstado": "${sala.Estado}"
            }
        """

        val request = Request.Builder()
            .url("${link}/Sala")
            .patch(jsonBody.toRequestBody("application/json; charset=utf-8".toMediaType()))
            .build()

        client.newCall(request).execute().use { response ->
            if(!response.isSuccessful) throw    IOException("Unexpected code $response")

            val result = response.body!!.string()

            val jsonObject = JSONObject(result)
            if(jsonObject.getString("statusCode") == "200"){
                return "Sala editada com sucesso"

            }else{
                return "Erro na edição"
            }
        }

    }

    fun addSala(scope:CoroutineScope, sala: Sala, callback: (String) -> Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val jsonBody = """
               {    
                    "nome": "${sala.nome}",
                    "idEstabelecimento": 1,
                    "idEstado": "${sala.idEstado}"
               } 
            """
            val request = Request.Builder().url("${link}/Sala")
                .post(jsonBody.toRequestBody("application/json; charset=utf-8".toMediaType()))
                .build()


                client.newCall(request).execute().use { response ->

                    if(!response.isSuccessful){
                        scope.launch(Dispatchers.Main){
                            callback("Error adding sala")
                        }
                    }
                    else if (response.code == 200){
                        scope.launch(Dispatchers.Main){
                            callback("Sucesso")
                        }
                    }
                    else{
                        throw IOException("Unexpected code $response")
                    }
                }

            }



    }

}
package com.example.monitumpdm

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody.Companion.toRequestBody
import org.json.JSONObject
import java.io.IOException

object ComunicadoRequests {
    private val client = OkHttpClient()

    fun getComunicadosByIdSala(scope: CoroutineScope, idSala: Int,  callback: (ArrayList<Comunicado>)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/ComunicadosByIdSala/sala/${idSala}").get().build()

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"){
                    var comunicados = arrayListOf<Comunicado>()
                    val comunicadoJSONData = jsonObject.getJSONArray("data")
                    for (i in 0 until comunicadoJSONData.length()){
                        val item = comunicadoJSONData.getJSONObject(i)
                        val comunicado = Comunicado.fromJSON(item)
                        comunicados.add(comunicado)
                    }
                    scope.launch(Dispatchers.Main){
                        callback(comunicados)
                    }

                }

            }
        }
    }

    fun deleteComunicado(scope: CoroutineScope, idComunicado: Int, token: String,  callback: (String)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/Comunicado?idComunicado=${idComunicado}")
                .delete()
                .addHeader("Authorization", " Bearer " + token)
                .build()

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                else if (response.code == 200){

                    scope.launch(Dispatchers.Main){
                        callback("Sucesso")
                    }
                }
                else {
                    throw IOException("Unexpected code $response")
                }

            }
        }
    }

    fun addComunicado(scope: CoroutineScope, comunicado: Comunicado, token: String, callback: (String)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val jsonBody = """
                {
                    "idSala": "${comunicado.idSala}",
                    "titulo": "${comunicado.titulo}",
                    "corpo": "${comunicado.corpo}"
                }
                """
            val request = Request.Builder().url("${link}/Comunicado")
                .post(jsonBody.toRequestBody("application/json; charset=utf-8".toMediaType()))
                .addHeader("Authorization", " Bearer " + token)
                .build()

            client.newCall(request).execute().use { response ->
                if (!response.isSuccessful){
                    scope.launch(Dispatchers.Main) {
                        callback("Error adding comunicado")
                    }
                }
                else if (response.code == 200){
                    //val result = response.body!!.string()

                    //val jsonObject = JSONObject(result)
                    //val JsonData = jsonObject.getString("data")

                    scope.launch(Dispatchers.Main){
                        callback("Sucesso")
                    }
                }
                else {
                    throw IOException("Unexpected code $response")
                }



            }
        }
    }
}
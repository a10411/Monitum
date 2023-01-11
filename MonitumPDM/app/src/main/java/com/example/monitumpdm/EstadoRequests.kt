package com.example.monitumpdm

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import org.json.JSONObject
import java.io.IOException

object  EstadoRequests {
    private val client = OkHttpClient()
    fun getAllEstados(scope:CoroutineScope, callback:(ArrayList<Estado>)->Unit){
        scope.launch(Dispatchers.IO){
          val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder()
                .url("${link}/Estado")
                .get()
                .build()

            client.newCall(request).execute().use{response->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()
                val jsonObject = JSONObject(result)
                if(jsonObject.getString("statusCode") == "200"){
                    var estados = arrayListOf<Estado>()
                    val estadoJSONData = jsonObject.getJSONArray("data")
                    for(i in 0 until  estadoJSONData.length()){
                        val item = estadoJSONData.getJSONObject(i)
                        val estado = Estado.fromJSON(item)
                        estados.add(estado)
                    }
                    scope.launch(Dispatchers.Main){
                        callback(estados)
                    }

                }

            }
        }
    }
}
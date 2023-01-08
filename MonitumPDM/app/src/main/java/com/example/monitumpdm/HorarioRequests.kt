package com.example.monitumpdm

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import org.json.JSONObject
import java.io.IOException

object HorarioRequests {
    private val client = OkHttpClient()

    fun getHorarios(idSala: Int,scope: CoroutineScope, callback: (ArrayList<Horario>)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/Horario_Sala/sala/${idSala}").get().build()

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"){
                    var horarios = arrayListOf<Horario>()
                    val horariosJSONData = jsonObject.getJSONArray("data")
                    // Log.d("HorarioRequests", horariosJSONData.toString())
                    for (i in 0 until horariosJSONData.length()){
                        val item = horariosJSONData.getJSONObject(i)
                        val horario = Horario.fromJSON(item)
                        horarios.add(horario)
                    }
                    scope.launch(Dispatchers.Main){
                        callback(horarios)
                    }
                }

            }
        }
    }
}
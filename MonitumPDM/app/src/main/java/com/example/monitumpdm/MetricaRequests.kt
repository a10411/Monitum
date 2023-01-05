package com.example.monitumpdm

import android.os.AsyncTask
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import org.json.JSONObject
import java.io.IOException

object MetricaRequests {
    private val client = OkHttpClient()

    fun getAllMetricas(scope: CoroutineScope, callback: (ArrayList<Metrica>)->Unit){
        scope.launch(Dispatchers.IO){
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder().url("${link}/Metrica").get().build()

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"){
                    var metricas = arrayListOf<Metrica>()
                    val metricaJSONData = jsonObject.getJSONArray("data")
                    for (i in 0 until metricaJSONData.length()){
                        val item = metricaJSONData.getJSONObject(i)
                        val metrica = Metrica.fromJSON(item)
                        metricas.add(metrica)
                    }
                    scope.launch(Dispatchers.Main){
                        callback(metricas)
                    }
                }

            }
        }
    }

    fun checkLastMetrica(idSala: Int, idMetrica: Int): Int? {
        if (idMetrica == 0){
            return null
        }
        val link = UtilsAPI().connectionNgRok()
        val request = Request.Builder().url("${link}/GetLastLogMetricaSala/sala/${idSala}/metrica/${idMetrica}").get().build()

        class CheckLastMetricaTask : AsyncTask<Void, Void, Int>() {
            override fun doInBackground(vararg params: Void?): Int? {
                val response = client.newCall(request).execute()
                if(!response.isSuccessful) throw IOException("Unexpected code ${response.code}")
                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"){
                    val metricaJSONData = jsonObject.getJSONObject("data")
                    return metricaJSONData.getInt("valorMetrica")
                } else {
                    return null
                }
            }
        }

        return CheckLastMetricaTask().execute().get()
    }
}
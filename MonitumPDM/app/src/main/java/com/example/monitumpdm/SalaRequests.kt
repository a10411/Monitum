package com.example.monitumpdm
import android.content.ContentValues.TAG
import android.security.KeyChainAliasCallback
import android.service.controls.ControlsProviderService.TAG
import android.util.Log
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.OkHttpClient
import okhttp3.Request
import org.json.JSONObject
import retrofit2.Callback
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.io.IOException
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext


object SalaRequests {
    private val client = OkHttpClient()

    fun getAllSalas(scope: CoroutineScope, callback: (ArrayList<Sala>)->Unit){
        scope.launch(Dispatchers.IO){
            val request = Request.Builder().url("meter Url do ngrok e o route presente no projeto").get().build()

            client.newCall(request).execute().use{ response ->
                if(!response.isSuccessful) throw IOException("Unexpected code $response")

                val result = response.body!!.string()

                val jsonObject = JSONObject(result)
                if (jsonObject.getString("statusCode") == "200"{
                        var clientes = arrayListOf<Sala>()
                        val SalaJSONData = jsonObject.getJSONObject("data")
                        val SalaJSONList = SalaJSONData.getJSONArray("value")

                        for (i in 0 until SalaJSONList.length()){
                            val item = SalaJSONList.getJSONObject(i)
                            val sala = Sala.fromJSON(item)
                            sala.add(sala)
                        }
                        scope.launch(Dispatchers.Main){
                            callback(sala)
                        }
                }

            }
        }
    }
}
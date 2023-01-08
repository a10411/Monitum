package com.example.monitumpdm

import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import okhttp3.MediaType.Companion.toMediaType
import okhttp3.Request
import okhttp3.RequestBody.Companion.toRequestBody
import org.json.JSONObject
import java.io.IOException
import okhttp3.OkHttpClient

object GestorRequests {
    private val client = OkHttpClient()
    fun loginGestor(scope: CoroutineScope, email : String?, password: String?, callback: (String) -> Unit){
        scope.launch(Dispatchers.IO) {
            val jsonBody = """
                {
                    "email": "$email",
                    "password": "$password"
                }
                """
            val link = UtilsAPI().connectionNgRok()
            val request = Request.Builder()
                .url("${link}/LoginGestor?email=${email}&password=${password}")
                .post(jsonBody.toRequestBody("application/json; charset=utf-8".toMediaType()))
                .build()

            client.newCall(request).execute().use { response ->
                if (!response.isSuccessful){
                    if (response.code == 404){
                        scope.launch(Dispatchers.Main) {
                            callback("User not found")
                        }
                    }
                } else {
                    throw IOException("Unexpected code $response")
                }


                val statusCode = response.code

                if(statusCode == 200) {
                    val result = response.body!!.string()

                    val jsonObject = JSONObject(result)
                    val JsonData = jsonObject.getString("data")

                    scope.launch(Dispatchers.Main){
                        callback(JsonData)
                    }
                }
                else {
                    scope.launch(Dispatchers.Main) {
                        callback("User not found")
                    }
                }
            }
        }
    }
}
package com.example.monitumpdm

import org.json.JSONObject
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.util.*

data class Comunicado(
    var idComunicado:Int? = null,
    var idSala: Int? = null,
    var titulo: String? = null,
    var corpo: String? = null,
    var dataHora: String? = null,
    var nomeSala: String? = null
)
{
    fun toJSON() : JSONObject {
        val jsonObj = JSONObject()
        var formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")
        val date = LocalDateTime.parse(dataHora.toString(), formatter)
        jsonObj.put("idComunicado", idComunicado)
        jsonObj.put("idSala", idSala)
        jsonObj.put("titulo", titulo)
        jsonObj.put("corpo", corpo)
        jsonObj.put("dataHora", date)

        return jsonObj
    }

    companion object{

        fun fromJSON(jsonObject: JSONObject) : Comunicado {
            var dateStr = jsonObject.getString("dataHora")
            var formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss")
            var date = LocalDateTime.parse(dateStr, formatter)
            var formatter2 = DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm")
            var date2 = date.format(formatter2)
            return Comunicado(
                jsonObject.getInt("idComunicado"),
                jsonObject.getInt("idSala"),
                jsonObject.getString("titulo"),
                jsonObject.getString("corpo"),
                date2
            )
        }
    }
}

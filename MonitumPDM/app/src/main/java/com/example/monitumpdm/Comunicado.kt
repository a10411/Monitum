package com.example.monitumpdm

import org.json.JSONObject
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.util.*

data class Comunicado(
    var idComunicado:Int? = null,
    var idSala: Int? = null,
    var titulo: String? = null,
    var corpo: String? = null,
    var dataHora: LocalDate? = null
)
{
    fun toJSON() : JSONObject {
        val jsonObj = JSONObject()
        jsonObj.put("idComunicado", idComunicado)
        jsonObj.put("idSala", idSala)
        jsonObj.put("titulo", titulo)
        jsonObj.put("corpo", corpo)
        jsonObj.put("dataHora", dataHora)

        return jsonObj
    }

    companion object{

        fun fromJSON(jsonObject: JSONObject) : Comunicado {
            var dateStr = jsonObject.getString("dataHora")
            var formatter = DateTimeFormatter.ofPattern("yyyy-mm-dd hh:mm:ss.fff")
            var date = LocalDate.parse(dateStr, formatter) // test
            return Comunicado(
                jsonObject.getInt("idComunicado"),
                jsonObject.getInt("idSala"),
                jsonObject.getString("titulo"),
                jsonObject.getString("corpo"),
                date
            )
        }
    }
}

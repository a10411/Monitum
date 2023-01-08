package com.example.monitumpdm

import org.json.JSONObject
import java.sql.Timestamp
import java.text.SimpleDateFormat
import java.time.LocalDate
import java.time.LocalTime
import java.time.format.DateTimeFormatter

data class Horario(
    var idSala:Int? = null,
    var diaSemana:String? = null,
    var horaEntrada:String? = null,
    var horaSaida:String? = null
)
{
    fun toJSON() : JSONObject{
        val jsonObj = JSONObject()
        jsonObj.put("idSala",idSala)
        jsonObj.put("diaSemana",diaSemana)
        jsonObj.put("horaEntrada",horaEntrada)
        jsonObj.put("horaSaida",horaSaida)

        return jsonObj
    }

    companion object {
        fun fromJSON(jsonObject: JSONObject) : Horario{
            val inputFormat = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss")
            val outputFormat = SimpleDateFormat("HH:mm")

            val dateStrEntrada = jsonObject.getString("horaEntrada")
            val dateStrSaida = jsonObject.getString("horaSaida")

            val horaEntrada = inputFormat.parse(dateStrEntrada)
            val horaSaida = inputFormat.parse(dateStrSaida)

            val outputEntrada = outputFormat.format(horaEntrada)
            val outputSaida = outputFormat.format(horaSaida)

            return Horario(
                jsonObject.getInt("idSala"),
                jsonObject.getString("diaSemana"),
                outputEntrada,
                outputSaida
            )
        }
    }
}
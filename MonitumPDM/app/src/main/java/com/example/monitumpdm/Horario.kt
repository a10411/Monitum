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

            var outputDia:String? = null

            if (jsonObject.getString("diaSemana") == "seg"){
                outputDia = "Segunda-feira"
            }else if (jsonObject.getString("diaSemana") == "ter") {
                outputDia = "Terça-feira"
            }else if (jsonObject.getString("diaSemana") == "qua"){
                outputDia = "Quarta-feira"
            }else if (jsonObject.getString("diaSemana") == "qui"){
                outputDia = "Quinta-feira"
            }else if (jsonObject.getString("diaSemana") == "sex"){
                outputDia = "Sexta-feira"
            }else if (jsonObject.getString("diaSemana") == "sab"){
                outputDia = "Sábado-feira"
            }else if (jsonObject.getString("diaSemana") == "dom"){
                outputDia = "Domingo-feira"
            }

            return Horario(
                jsonObject.getInt("idSala"),
                outputDia,
                outputEntrada,
                outputSaida
            )
        }
    }
}
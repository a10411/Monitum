package com.example.monitumpdm

import org.json.JSONObject

data class Estado (
    var idEstado:Int? = null,
    var estado:String? = null
)
{
    fun toJSON() : JSONObject{
        val jsonObj = JSONObject()
        jsonObj.put("idEstado", idEstado)
        jsonObj.put("nome_Estado", estado)

        return jsonObj
    }

    companion object{

        fun fromJSON(jsonObject: JSONObject): Estado{
            return Estado(
                jsonObject.getInt("idEstado"),
                jsonObject.getString("nome_Estado")
            )
        }
    }
}



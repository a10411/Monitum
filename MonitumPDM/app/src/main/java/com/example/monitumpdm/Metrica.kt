package com.example.monitumpdm

import org.json.JSONObject

data class Metrica(
    var idMetrica:Int? = null,
    var nome:String? = null,
    var medida:String? =  null,
    var valor:Int? = null // for Front end purposes
)
{
    fun toJSON() : JSONObject {
        val jsonObj = JSONObject()
        jsonObj.put("idMetrica", idMetrica)
        jsonObj.put("nome", nome)
        jsonObj.put("medida", medida)

        return jsonObj
    }

    companion object{

        fun fromJSON(jsonObject: JSONObject) : Metrica {
            return Metrica(
                jsonObject.getInt("idMetrica"),
                jsonObject.getString("nome"),
                jsonObject.getString("medida")
            )
        }
    }
}

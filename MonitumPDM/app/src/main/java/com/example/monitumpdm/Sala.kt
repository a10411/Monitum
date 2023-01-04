package com.example.monitumpdm
import org.json.JSONObject

data class Sala(
    val idSala:Int? = null,
    val nome:String? = null,
    val idEstabelecimento:Int? = null,
    val idEstado:Int? = null,
    var Estado:String? = null
)
{
    fun toJSON() : JSONObject{
        val jsonObj = JSONObject()
        jsonObj.put("idSala", idSala)
        jsonObj.put("nome", nome)
        jsonObj.put("idEstabelecimento", idEstabelecimento)
        jsonObj.put("idEstado", idEstado)

        return jsonObj
    }

    companion object{

        fun fromJSON(jsonObject: JSONObject) : Sala {
            return Sala(
                jsonObject.getInt("idSala"),
                jsonObject.getString("nome"),
                jsonObject.getInt("idEstabelecimento"),
                jsonObject.getInt("idEstado")
            )
        }
    }
}


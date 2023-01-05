package com.example.monitumpdm
import org.json.JSONObject

data class Sala(
    var idSala:Int? = null,
    var nome:String? = null,
    var idEstabelecimento:Int? = null,
    var idEstado:Int? = null,
    var Estado:String? = null // for Front end purposes
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


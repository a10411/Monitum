package com.example.monitumpdm
import org.json.JSONObject

data class Sala(
    val id_sala:Int? = null,
    val id_estabelecimento:Int? = null,
    val id_estado:Int? = null
)
{
    fun toJSON() : JSONObject{
        val jsonObj = JSONObject()
        jsonObj.put("id_sala", id_sala)
        jsonObj.put("id_estabelecimento", id_estabelecimento)
        jsonObj.put("id_estado", id_estado)

        return jsonObj
    }

    companion object{

        fun fromJSON(jsonObject: JSONObject) : Sala {
            return Sala(
                jsonObject.getInt("id_sala"),
                jsonObject.getInt("id_estabelecimento"),
                jsonObject.getInt("id_estado")

            )
        }
    }
}


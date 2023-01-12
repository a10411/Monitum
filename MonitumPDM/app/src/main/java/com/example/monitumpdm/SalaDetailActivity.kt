package com.example.monitumpdm

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.lifecycle.lifecycleScope
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

class SalaDetailActivity : AppCompatActivity() {

    var metricas = arrayListOf<Metrica>()

    val adapter = MetricasAdapter()

    val sala = Sala(null, null, null, null, null)

    val comunicados = arrayListOf<Comunicado>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_sala_detail)

        val idSala = intent.getIntExtra("idSala", 0)
        val nome = intent.getStringExtra("nome")
        val idEstabelecimento = intent.getIntExtra("idEstabelecimento", 0)
        val idEstado = intent.getIntExtra("idEstado", 0)
        val estado = intent.getStringExtra("Estado")

        findViewById<TextView>(R.id.textViewNomeSalaSalaDetail).text = nome


        sala.idSala = idSala
        sala.nome = nome
        sala.idEstabelecimento = idEstabelecimento
        sala.idEstado = idEstado
        sala.Estado = estado

        val listViewMetricas = findViewById<ListView>(R.id.listViewInfoSalaDetailGestor)
        listViewMetricas.adapter = adapter

        metricas.add(Metrica(0, "Estado", null, if (estado == "Aberta") 1 else 0))
        MetricaRequests.getAllMetricas(lifecycleScope){
            for (metrica in it){
                metrica.valor = MetricaRequests.checkLastMetrica(idSala, metrica.idMetrica!!)
            }
            metricas += it
            adapter.notifyDataSetChanged()
        }

        ComunicadoRequests.getComunicadosByIdSala(lifecycleScope, sala.idSala!!){
            for (comunicado in it){
                var formatter2 = DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm")
                var date2 = comunicado.dataHora
                var dttime = LocalDateTime.parse(date2, formatter2)
                if (dttime >= LocalDateTime.now().minusDays(2)){
                    Toast(this).showCustomToast("Título: ${comunicado.titulo}\nCorpo: ${comunicado.corpo}", this)
                    //Toast.makeText(this,comunicado.titulo + "\n" + comunicado.corpo, Toast.LENGTH_SHORT).show()
                    //Log.d("TAG", comunicado.titulo!!)
                }
            }
        }


        val buttonVerHorario: Button = findViewById(R.id.buttonVerHorarioSalaDetail)
        buttonVerHorario.setOnClickListener{
            val intent = Intent(this, HorarioActivity::class.java)
            intent.putExtra("idSala", sala.idSala)
            intent.putExtra("nome", sala.nome)
            startActivity(intent)
        }
    }

    inner class MetricasAdapter: BaseAdapter(){
        override fun getCount(): Int {
            return metricas.size
        }

        override fun getItem(pos: Int): Any {
            return  metricas[pos]
        }

        override fun getItemId(p0: Int): Long {
            return 0L
        }

        override fun getView(pos: Int, view: View?, parent: ViewGroup?): View {
            val rootView = layoutInflater.inflate(R.layout.row_sala, parent, false)

            val textViewNomeSala = rootView.findViewById<TextView>(R.id.textViewNomeSala)
            val textViewEstadoSala = rootView.findViewById<TextView>(R.id.textViewEstadoSala)

            textViewNomeSala.text = metricas[pos].nome

            val salaStatusColor = rootView.findViewById<SalaStatusColor>(R.id.salaStatusColor)

            if (metricas[pos].nome == "Estado"){
                if (metricas[pos].valor == 1){
                    textViewEstadoSala.text = "Aberta"
                    salaStatusColor.color = "#3EE723"
                } else {
                    textViewEstadoSala.text = "Fechada"
                    salaStatusColor.color = "#F53333"
                }
            } else {
                salaStatusColor.color = "#3EE723"
                textViewEstadoSala.text = "${metricas[pos].valor.toString()} ${metricas[pos].medida.toString()}"
                if (metricas[pos].valor!! > 15){
                    salaStatusColor.color = "#E5DE2F"
                }
                else if (metricas[pos].valor !! > 30){
                    salaStatusColor.color = "#F53333"
                }

            }

            return rootView

        }

    }
}
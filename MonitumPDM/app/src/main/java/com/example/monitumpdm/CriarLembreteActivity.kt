package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.View
import android.widget.*
import com.google.gson.Gson


class CriarLembreteActivity : AppCompatActivity() {

    val lembrete = Lembrete(null, null, null)

    var lembreteList = arrayListOf<Lembrete>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_criar_lembrete)

        val idSala = intent.getIntExtra("idSala", 0)
        val nome = intent.getStringExtra("nome")

        val spinner1: Spinner = findViewById(R.id.spinnerMetricaCriarLembrete)
        val items1 = arrayOf("Ocupacao", "Ruido")
        val adapter = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item, items1)
        spinner1.adapter = adapter

        val spinner2: Spinner = findViewById(R.id.spinnerAbaixoCriarLembrete)
        val items2 = arrayOf("Abaixo", "Acima")
        val adapter2 = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item, items2)
        spinner2.adapter = adapter2

        findViewById<TextView>(R.id.textViewNomeSalaCriarLembrete).text = nome

        val editTextValor = findViewById<EditText>(R.id.editTextValorMetricaCriarLembrete)

        editTextValor.addTextChangedListener(object : TextWatcher {
            override fun afterTextChanged(s: Editable?) {
            }

            override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {
            }

            override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
                lembrete.valorNecessario = s.toString()
            }
        })


        spinner1.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {
                lembrete.nomeMetrica = spinner1.selectedItem.toString()
            }
            override fun onNothingSelected(p0: AdapterView<*>?) {

            }
        }

        spinner2.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {
                if (spinner2.selectedItem.toString() == "Abaixo") {
                    lembrete.maiorMenor = "<"
                }else if(spinner2.selectedItem.toString() == "Acima"){
                    lembrete.maiorMenor = ">"
                }
            }
            override fun onNothingSelected(p0: AdapterView<*>?) {

            }
        }

        val buttonCriarLembrete: Button = findViewById(R.id.buttonCriarLembrete)
        buttonCriarLembrete.setOnClickListener{
            val sharedPref = getSharedPreferences("lembrete",Context.MODE_PRIVATE)
            val editor = sharedPref.edit()
            val gson = Gson()
            lembreteList.add(Lembrete(lembrete.nomeMetrica,lembrete.maiorMenor,lembrete.valorNecessario))
            val json = gson.toJson(lembreteList)
            editor.putString("array_list", json)
            editor.apply()
        }

        val buttonCancelarDefinirLembreteActivity: Button = findViewById(R.id.buttonCancelarDefinirLembrete)
        buttonCancelarDefinirLembreteActivity.setOnClickListener{
            val intent = Intent(this, SalaDetailActivity::class.java)
            intent.putExtra("idSala", idSala)
            intent.putExtra("nome", nome)
            startActivity(intent)
        }
    }
}
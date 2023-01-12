package com.example.monitumpdm

import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.Context
import android.content.Intent
import android.os.Build
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.view.ViewGroup
import android.widget.*
import androidx.core.app.NotificationCompat
import androidx.lifecycle.lifecycleScope
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken

class VerLembreteActivity : AppCompatActivity() {
    val lembretesType = object : TypeToken<ArrayList<Lembrete>>() {}.type
    var lembretes = ArrayList<Lembrete>()
    val adapter = LembreteAdapter()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_ver_lembrete)

        val idSala = intent.getIntExtra("idSala", 0)
        val nome = intent.getStringExtra("nome")

        val sharedPref = getSharedPreferences("lembrete", Context.MODE_PRIVATE)
        val gson = Gson()
        val json = sharedPref.getString("array_list", null)
        if (json != null) {
            lembretes = gson.fromJson(json, lembretesType)
        }

        findViewById<TextView>(R.id.textViewNomeSalaVerLembrete).text = nome

        val listViewLembrete = findViewById<ListView>(R.id.listViewVerLembrete)
        listViewLembrete.adapter = adapter

        fun sendPushNotification(position: Int) {
            // Retrieve the switch at the selected position
            val switch = listViewLembrete.getChildAt(position).findViewById<Switch>(R.id.switch1)
            if (switch.isChecked) {
                val notificationManager =
                    getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
                val notificationID = 101
                val channelID = "channel1"
                val name = getString(R.string.channel_name)
                val descriptionText = getString(R.string.channel_description)
                val importance = NotificationManager.IMPORTANCE_HIGH

                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                    val mChannel = NotificationChannel(channelID, name, importance)
                    mChannel.description = descriptionText
                    notificationManager.createNotificationChannel(mChannel)
                }
                val notificationBuilder = NotificationCompat.Builder(this, channelID)
                    .setContentTitle("My Notification")
                    .setContentText("This is my notification text.")
                    .setAutoCancel(true)
                    .setWhen(System.currentTimeMillis() + 5 * 1000)

                notificationManager.notify(notificationID, notificationBuilder.build())
            }
        }

        listViewLembrete.onItemClickListener =
            AdapterView.OnItemClickListener { _, view, position, _ ->
                val switch = view.findViewById<Switch>(R.id.switch1)
                switch.setOnCheckedChangeListener { _, isChecked ->
                    sendPushNotification(position)
                }
            }

        val buttonHorarioActivity: Button = findViewById(R.id.buttonVerHorarioVerLembrete)
        buttonHorarioActivity.setOnClickListener {
            val intent = Intent(this, HorarioActivity::class.java)
            intent.putExtra("idSala", idSala)
            intent.putExtra("nome", nome)
            startActivity(intent)
        }

        val buttonCriarLembreteActivity: Button =
            findViewById(R.id.buttonDefinirLembreteVerLembrete)
        buttonCriarLembreteActivity.setOnClickListener {
            val intent = Intent(this, CriarLembreteActivity::class.java)
            intent.putExtra("idSala", idSala)
            intent.putExtra("nome", nome)
            startActivity(intent)
        }
    }

    inner class LembreteAdapter : BaseAdapter() {
        override fun getCount(): Int {
            return lembretes.size
        }

        override fun getItem(pos: Int): Any {
            return lembretes[pos]
        }

        override fun getItemId(p0: Int): Long {
            return 0L
        }

        override fun getView(pos: Int, view: View?, parent: ViewGroup?): View {
            val rootView = layoutInflater.inflate(R.layout.row_lembrete, parent, false)

            val textViewRowLembrete = rootView.findViewById<TextView>(R.id.textViewRowLembrete)

            textViewRowLembrete.text =
                "${lembretes[pos].nomeMetrica} ${lembretes[pos].maiorMenor ?: "N/A"} ${lembretes[pos].valorNecessario ?: "N/A"}"

            return rootView
        }
    }
}
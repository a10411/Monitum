package com.example.monitumpdm

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import org.json.JSONObject

class LoginGestorActivity : AppCompatActivity() {

    private lateinit var emailEditText: EditText
    private lateinit var passwordEditText: EditText
    private lateinit var loginButton: Button

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login_gestor)


        emailEditText = findViewById<EditText>(R.id.editTextEmail)
        passwordEditText = findViewById<EditText>(R.id.editTextPassword)
        loginButton = findViewById<Button>(R.id.buttonLogin)



        loginButton.setOnClickListener{
            val email = emailEditText.text.toString()
            val password = passwordEditText.text.toString()

            val scope = CoroutineScope(Dispatchers.Main)
            GestorRequests.loginGestor(scope,email,password){result ->
                if(result == "User not found"){
                    Toast(this).showCustomToast("Credenciais erradas!", this)
                    //Toast.makeText(this,"User not found", Toast.LENGTH_LONG).show()
                }else{
                    //Toast.makeText(this, "Success!!", Toast.LENGTH_LONG).show()
                    val preferences = getSharedPreferences("my_preferences", Context.MODE_PRIVATE)
                    val editor = preferences.edit()
                    editor.putString("session_token", result)
                    editor.apply()

                    val intent = Intent(this@LoginGestorActivity, MenuPrincipalGestorActivity::class.java)
                    // Toast.makeText(this, result, Toast.LENGTH_LONG).show() // Comentar
                    startActivity(intent)
                }
            } }
    }


}
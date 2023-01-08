package com.example.monitumpdm

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers

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
                    Toast.makeText(this,"User not found", Toast.LENGTH_LONG).show()
                }else{
                    Toast.makeText(this, "Success!!", Toast.LENGTH_LONG).show()
                    //startActivity(Intent(this, MenuPrincipalGestorActicity::class.java))
                }
            }
        }
    }


}
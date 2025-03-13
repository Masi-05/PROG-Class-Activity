package vcmsa.projects.wordleapplication

import android.graphics.Color
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.View
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import vcmsa.projects.wordleapplication.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding
    private var wordToGuess: String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        fetchWordFromAPI()

        keepPassingFocus()
        setupTextWatchers()
    }

    private fun fetchWordFromAPI() {
        val retrofit = Retrofit.Builder()
            .baseUrl("https://wordle20250313105430.azurewebsites.net/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val apiService = retrofit.create(WordleApi::class.java)

        apiService.getWord().enqueue(object : Callback<WordResponse> {
            override fun onResponse(call: Call<WordResponse>, response: Response<WordResponse>) {
                if (response.isSuccessful && response.body() != null) {
                    wordToGuess = response.body()!!.word.uppercase()
                } else {
                    Toast.makeText(applicationContext, "Failed to fetch word", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<WordResponse>, t: Throwable) {
                Toast.makeText(applicationContext, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun setupTextWatchers() {
        val lastEdits = listOf(
            binding.edt15, binding.edt25, binding.edt35, binding.edt45, binding.edt55, binding.edt65
        )

        lastEdits.forEach { editText ->
            editText.addTextChangedListener(object : TextWatcher {
                override fun beforeTextChanged(s: CharSequence?, start: Int, count: Int, after: Int) {}
                override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {}
                override fun afterTextChanged(s: Editable?) {
                    if (s?.length == 1) {
                        validateRow()
                    }
                }
            })
        }
    }

    private fun validateRow() {
        if (wordToGuess.isEmpty()) {
            Toast.makeText(applicationContext, "Word not loaded yet!", Toast.LENGTH_SHORT).show()
            return
        }

        val editTexts = listOf(
            binding.edt11, binding.edt12, binding.edt13, binding.edt14, binding.edt15
        )

        val userGuess = editTexts.joinToString("") { it.text.toString().uppercase() }

        editTexts.forEachIndexed { index, editText ->
            when {
                userGuess[index] == wordToGuess[index] -> editText.setBackgroundColor(Color.GREEN)
                wordToGuess.contains(userGuess[index]) -> editText.setBackgroundColor(Color.YELLOW)
                else -> editText.setBackgroundColor(Color.RED)
            }
        }

        if (userGuess == wordToGuess) {
            binding.idTVCongo.text = "Congratulations! You guessed the word!"
            binding.idTVCongo.visibility = View.VISIBLE
            makeGameInactive()
        }
    }

    private fun makeGameInactive() {
        listOf(
            binding.edt11, binding.edt12, binding.edt13, binding.edt14, binding.edt15,
            binding.edt21, binding.edt22, binding.edt23, binding.edt24, binding.edt25,
            binding.edt31, binding.edt32, binding.edt33, binding.edt34, binding.edt35,
            binding.edt41, binding.edt42, binding.edt43, binding.edt44, binding.edt45,
            binding.edt51, binding.edt52, binding.edt53, binding.edt54, binding.edt55,
            binding.edt61, binding.edt62, binding.edt63, binding.edt64, binding.edt65
        ).forEach { it.isEnabled = false }
    }

    private fun keepPassingFocus() {
        listOf(
            binding.edt11 to binding.edt12, binding.edt12 to binding.edt13, binding.edt13 to binding.edt14, binding.edt14 to binding.edt15,
            binding.edt21 to binding.edt22, binding.edt22 to binding.edt23, binding.edt23 to binding.edt24, binding.edt24 to binding.edt25,
            binding.edt31 to binding.edt32, binding.edt32 to binding.edt33, binding.edt33 to binding.edt34, binding.edt34 to binding.edt35,
            binding.edt41 to binding.edt42, binding.edt42 to binding.edt43, binding.edt43 to binding.edt44, binding.edt44 to binding.edt45,
            binding.edt51 to binding.edt52, binding.edt52 to binding.edt53, binding.edt53 to binding.edt54, binding.edt54 to binding.edt55,
            binding.edt61 to binding.edt62, binding.edt62 to binding.edt63, binding.edt63 to binding.edt64, binding.edt64 to binding.edt65
        ).forEach { (current, next) ->
            current.setOnKeyListener { _, _, _ ->
                if (current.text.length == 1) next.requestFocus()
                false
            }
        }
    }
}

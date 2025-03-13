package vcmsa.projects.wordleapplication

import retrofit2.Call
import retrofit2.http.GET

interface WordleApi {
    @GET("api/wordie/word")
    fun getWord(): Call<WordResponse>
}

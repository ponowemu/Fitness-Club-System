package pl.smartica.trimfitmobile.data

import android.content.Context
import android.util.Log
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonObjectRequest
import org.json.JSONObject
import pl.smartica.trimfitmobile.Callback
import pl.smartica.trimfitmobile.data.model.LoggedInUser
import java.io.BufferedReader
import java.io.IOException
import java.io.InputStreamReader
import java.io.OutputStreamWriter
import java.lang.Exception
import java.net.HttpURLConnection
import java.net.URL
import java.net.URLEncoder
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine



/**
 * Class that handles authentication w/ login credentials and retrieves user information.
 */
class LoginDataSource {
    lateinit var result: Result<LoggedInUser>

    suspend fun login(username: String, password: String, context: Context) = suspendCoroutine<Result<LoggedInUser>> {cont->
        try {
            // TODO: handle loggedInUser authentication
      /*      val params = HashMap<String, String>()
            params["user_login"] = username
            params["user_password"] = "dupa"*/
            Log.v("USER:", username)
            Log.v("Password: ", password)
            val jsonBody = JSONObject()
            jsonBody.put("user_login", username)
            jsonBody.put("user_password", password)
            val queue = Callback.getInstance(context).requestQueue
            val url = "http://api.trimfit.pl/api/Users/Login"
            val stringReq = JsonObjectRequest(
                Request.Method.POST,url, jsonBody,
                Response.Listener<JSONObject> { response ->
                    val User = LoggedInUser(java.util.UUID.randomUUID().toString(), "ponowemu")
                    Log.v("RESPONE",response.toString())
                    cont.resume(Result.Success(User))
                },
                Response.ErrorListener { error->
                    if (error.networkResponse == null)
                        Log.v("TAG", "NULL LUL: " + error.toString())
                    Log.v("TAG", "ERROR LUL: " + error.toString())
                    cont.resume( Result.Error(Exception("Wrong password")))
                })

            Callback.getInstance(context).addToRequestQueue(stringReq)
        } catch (e: Throwable) {
            Log.v("problem http request" , e.toString())
            cont.resume( Result.Error(IOException("Error logging in", e)))
        }
    }

            fun logout() {
        // TODO: revoke authentication
    }
}


package pl.smartica.trimfitmobile.data

import android.content.Context
import android.util.Log
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonObjectRequest
import org.json.JSONObject
import pl.smartica.trimfitmobile.Callback
import pl.smartica.trimfitmobile.data.model.LoggedInUser
import java.io.IOException
import java.lang.Exception

/**
 * Class that handles authentication w/ login credentials and retrieves user information.
 */
class LoginDataSource {

    fun login(username: String, password: String): Result<LoggedInUser> {
        try {
            // TODO: handle loggedInUser authentication

            val fakeUser = LoggedInUser(java.util.UUID.randomUUID().toString(), "Jane Doe")
        /*  val jsonBody = JSONObject()
            jsonBody.put("user_login", "ponowemu")
            jsonBody.put("password", "dupa")
            val queue = Callback.getInstance(context).requestQueue
            val url = "http://api.trimfit.pl/api/Users/Login"
            val stringReq = JsonObjectRequest(
                Request.Method.POST,url,jsonBody,
                Response.Listener<JSONObject> { response ->

                    Log.d("RESPONE",response.toString())
                },
                Response.ErrorListener { error->
                    Log.v("TAG", "ERROR LUL: " + error.toString())

                })
            Callback.getInstance(context).addToRequestQueue(stringReq)*/
            return Result.Success(fakeUser)
        } catch (e: Throwable) {
            return Result.Error(IOException("Error logging in", e))
        }
    }

    fun logout() {
        // TODO: revoke authentication
    }
}


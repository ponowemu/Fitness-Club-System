package pl.smartica.trimfitmobile

import android.app.DownloadManager
import android.content.Context
import android.util.Log
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.*
import org.json.JSONArray
import org.json.JSONObject

class Tester {
    public fun sendRequest(context: Context)
    {
        // Instantiate the RequestQueue.
        val queue = Callback.getInstance(context).requestQueue
        val url = "http://api.trimfit.pl/api/Services"
        val stringReq = JsonArrayRequest(Request.Method.GET,url,null,
            Response.Listener<JSONArray> {
                    response -> Log.d("RESPONE",response.toString())

            },Response.ErrorListener {
                    error ->  Log.v("TAG", "ERROR LUL: " + error.toString())
            })

        Callback.getInstance(context).addToRequestQueue(stringReq)

    }
    fun parseJson(response: JSONArray)
    {
        Log.v("JSON",response.toString())
        for (item in 0..response.length()-1)
        {
            Log.d("Header",response.getJSONObject(item).getString("service_Name"))
            Log.d("Description",response.getJSONObject(item).getString("service_Description"))
        }
    }
}
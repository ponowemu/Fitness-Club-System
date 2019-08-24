package pl.smartica.trimfitmobile

import android.content.Context
import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import com.android.volley.Request
import com.android.volley.Response
import org.json.JSONArray
import org.json.JSONObject
import pl.smartica.trimfitmobile.Callback
import pl.smartica.trimfitmobile.CustomRequests.CustomJsonArrayRequest
import pl.smartica.trimfitmobile.CustomRequests.CustomJsonObjectRequest
import pl.smartica.trimfitmobile.data.Result
import pl.smartica.trimfitmobile.data.model.LoggedInUser
import pl.smartica.trimfitmobile.model.Service
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

class ApiCall {
    var liveError =  MutableLiveData<Boolean>()
    var liveItems =  MutableLiveData<JSONArray>()
    var liveItem =  MutableLiveData<JSONObject>()
    val baseUrl: String = "http://api.trimfit.pl/api/"
    var accessToken: String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NjYxNTMyNjMsImV4cCI6MTU2ODc0NTI2MywiaWF0IjoxNTY2MTUzMjYzfQ.gLZArPDFrWes9xgsCzzc5_IEDi-xmCtTfroVeafCgac"

    suspend fun getItemsFromApi(context: Context,url: String,params: JSONArray?) = suspendCoroutine<JSONArray?> { cont ->
        try{
            // Instatiate the RequestQueue.
            val queue = Callback.getInstance(context)
            var code = 0
            val stringReq = CustomJsonArrayRequest(
                Request.Method.GET, baseUrl + url, params,
                Response.Listener<JSONArray> { response ->
                    cont.resume(response)
                    Log.v("TAG", "STATUS CODE: " + code)
                },
                Response.ErrorListener { error ->
                    cont.resume(null)
                    Log.v("TAG", "ERROR LUL: " + error.toString())
                }, accessToken,code)
            queue.addToRequestQueue(stringReq)
        }
        catch (e:Throwable){
            cont.resume(null)
        }
    }

    suspend fun getSingleItemFromApi(context: Context,url: String,params: JSONObject?) = suspendCoroutine<JSONObject?> { cont ->
        try {
            val queue = Callback.getInstance(context)
            var code = 0
            val stringReq = CustomJsonObjectRequest(
                Request.Method.GET, baseUrl + url, params,
                Response.Listener<JSONObject> { response ->
                    cont.resume(response)
                    Log.v("TAG", "STATUS CODE: " + code)
                },
                Response.ErrorListener { error ->
                    cont.resume(null)
                    Log.v("TAG", "ERROR LUL: " + error.toString())
                }, accessToken, code
            )
            queue.addToRequestQueue(stringReq)
        } catch (e: Throwable) {
            cont.resume(null)
        }
    }

        // Instantiate the RequestQueue.

    fun convertData(array: JSONArray)
    {

    }


}
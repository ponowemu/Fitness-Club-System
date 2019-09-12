package pl.smartica.trimfitmobile.api

import android.content.Context
import android.util.Log
import androidx.lifecycle.MutableLiveData
import com.android.volley.Request
import com.android.volley.Response
import org.json.JSONArray
import org.json.JSONObject
import pl.smartica.trimfitmobile.api.CustomRequests.CustomJsonArrayRequest
import pl.smartica.trimfitmobile.api.CustomRequests.CustomJsonObjectRequest
import kotlin.coroutines.resume
import kotlin.coroutines.suspendCoroutine

class ApiCall {
    val baseUrl: String = "http://api.trimfit.pl/api/"
    var accessToken: String = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NjYxNTMyNjMsImV4cCI6MTU2ODc0NTI2MywiaWF0IjoxNTY2MTUzMjYzfQ.gLZArPDFrWes9xgsCzzc5_IEDi-xmCtTfroVeafCgac"

    suspend fun getItemsFromApi(context: Context,method: String ,url: String,params: JSONArray?) = suspendCoroutine<JSONArray?> { cont ->
        try{
            val queue = Callback.getInstance(context)
            var code = 0
            val stringReq = CustomJsonArrayRequest(
                getMethod(method), baseUrl + url, params,
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

    suspend fun getSingleItemFromApi(context: Context,method: String  ,url: String,params: JSONObject?) = suspendCoroutine<JSONObject?> { cont ->
        try {
            val queue = Callback.getInstance(context)
            var code = 0
            val stringReq = CustomJsonObjectRequest(
                getMethod(method), baseUrl + url, params,
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

    fun getMethod(method: String): Int
    {
        var meth = Request.Method.GET
        when(method.toLowerCase())
        {
            "get"-> meth = Request.Method.GET
            "post"-> meth = Request.Method.POST
            "put"-> meth = Request.Method.PUT
            "delete"-> meth = Request.Method.DELETE
        }
        return meth
    }


}
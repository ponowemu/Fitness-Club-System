package pl.smartica.trimfitmobile.api.CustomRequests

import com.android.volley.AuthFailureError
import com.android.volley.NetworkResponse
import com.android.volley.Response
import com.android.volley.toolbox.JsonObjectRequest
import org.json.JSONObject
import kotlin.collections.HashMap

class CustomJsonObjectRequest(
    method:Int, url: String,
    jsonObject: JSONObject?,
    listener: Response.Listener<JSONObject>,
    errorListener: Response.ErrorListener,
    token:String,
    statusCode: Int
) : JsonObjectRequest(method,url, jsonObject, listener, errorListener) {
    var statusCode: Int? = 0
    private var tokenApi:String = token

    @Throws(AuthFailureError::class)
    override fun getHeaders(): Map<String, String> {
        val headers = HashMap<String, String>()
        headers["Content-Type"] = "application/json"
        val auth = "Bearer " + tokenApi
        headers["Authorization"] = auth
        return headers
    }

    override fun parseNetworkResponse(response: NetworkResponse?): Response<JSONObject> {
        if (response != null)
            statusCode = response.statusCode
        return super.parseNetworkResponse(response)
    }
}


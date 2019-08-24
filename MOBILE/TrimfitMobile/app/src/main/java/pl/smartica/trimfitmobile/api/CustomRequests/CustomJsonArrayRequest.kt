package pl.smartica.trimfitmobile.CustomRequests

import com.android.volley.AuthFailureError
import com.android.volley.NetworkResponse
import com.android.volley.Response
import com.android.volley.toolbox.JsonArrayRequest
import org.json.JSONArray
import org.json.JSONObject

class CustomJsonArrayRequest(
    method:Int, url: String,
    jsonArray: JSONArray?,
    listener: Response.Listener<JSONArray>,
    errorListener: Response.ErrorListener,
    token:String,
    statusCode: Int
)
    : JsonArrayRequest(method,url, jsonArray, listener, errorListener) {
    private var tokenApi:String = token
    var statusCode: Int? = 0
    override fun parseNetworkResponse(response: NetworkResponse?): Response<JSONArray> {
        if (response != null)
            statusCode = response.statusCode
        return super.parseNetworkResponse(response)
    }

    @Throws(AuthFailureError::class)
    override fun getHeaders(): Map<String, String> {
        val headers = HashMap<String, String>()
        headers["Content-Type"] = "application/json"
        val auth = "Bearer " + tokenApi
        headers["Authorization"] = auth
        return headers
    }
}

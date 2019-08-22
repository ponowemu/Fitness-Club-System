package pl.smartica.trimfitmobile.repositories

import android.content.Context
import android.util.Log
import androidx.lifecycle.MutableLiveData
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonArrayRequest
import org.json.JSONArray
import pl.smartica.trimfitmobile.Callback
import pl.smartica.trimfitmobile.model.Service

class ServicesRepository {
    private var mServiceList=  MutableLiveData<MutableList<Service>>()
    private var serviceList: MutableList<Service> = mutableListOf()
    fun getServiceList(context: Context): MutableLiveData<MutableList<Service>>{
        getServicesFromAPI(context)
        mServiceList.value = serviceList
        return mServiceList
    }

    fun getServicesFromAPI(context: Context){

        // Instantiate the RequestQueue.
        val queue = Callback.getInstance(context)
        val url = "http://api.trimfit.pl/api/Services"
        val stringReq = JsonArrayRequest(
            Request.Method.GET,url,null,
            Response.Listener<JSONArray> {response ->
                convertJsonToItems(response)
                Log.d("RESPONE: ",response.toString())
            },
            Response.ErrorListener {
                    error ->  Log.v("TAG", "ERROR LUL: " + error.toString())
            })
        queue.addToRequestQueue(stringReq)
    }

    fun convertJsonToItems(jsonArray: JSONArray){
        if (jsonArray != null)
        {
            for (i in 0..jsonArray!!.length()-1)
            {
                val item = jsonArray.getJSONObject(i)
                serviceList.add(Service(item))
            }
            mServiceList.value = serviceList
        }
    }

}
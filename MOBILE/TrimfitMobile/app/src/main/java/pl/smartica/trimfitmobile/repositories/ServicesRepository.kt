package pl.smartica.trimfitmobile.repositories

import pl.smartica.trimfitmobile.api.ApiCall
import android.content.Context
import android.util.Log
import androidx.lifecycle.MutableLiveData
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.launch
import org.json.JSONArray
import pl.smartica.trimfitmobile.api.model.Service

class ServicesRepository {
    private var mServiceList=  MutableLiveData<MutableList<Service>>()
    private var serviceList: MutableList<Service> = mutableListOf()
    fun getServiceList(context: Context): MutableLiveData<MutableList<Service>>{
        GlobalScope.launch {
            var items = ApiCall()
                .getItemsFromApi(context, "get","Services", null)
            if (items != null)
                convertJsonToItems(items)
            Log.v("FINISHED", "CHECKUP")
        }
       // getServicesFromAPI(context)
        mServiceList.value = serviceList
        return mServiceList
    }

    fun convertJsonToItems(jsonArray: JSONArray){
        if (jsonArray != null)
        {
            for (i in 0..jsonArray!!.length()-1)
            {
                val item = jsonArray.getJSONObject(i)
                serviceList.add(Service(item))
            }
            mServiceList.postValue(serviceList)
        }
    }

}
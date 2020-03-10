package pl.smartica.trimfitmobile.repositories

import android.content.Context
import android.util.Log
import androidx.lifecycle.MutableLiveData
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import org.json.JSONArray
import pl.smartica.trimfitmobile.api.ApiCall
import pl.smartica.trimfitmobile.api.model.Event
import pl.smartica.trimfitmobile.api.model.Service

class CalendarRepository {
    private var mEventList=  MutableLiveData<MutableList<Event>>()
    private var itemList: MutableList<Event> = mutableListOf()
    fun getItems(context: Context):MutableLiveData<MutableList<Event>>{
        GlobalScope.launch {
            var items = ApiCall()
                .getItemsFromApi(context, "get","TimetableActivities?incoming=true&related=true&timetable=20", null)
            if (items != null)
                convertJsonToItems(items)
            Log.v("FINISHED timetable ", "CHECKUP activites")
        }
        // getServicesFromAPI(context)
        mEventList.value =itemList
        return mEventList
    }

    fun convertJsonToItems(jsonArray: JSONArray){
        if (jsonArray != null)
        {
            for (i in 0..jsonArray!!.length()-1)
            {
                val item = jsonArray.getJSONObject(i)
                itemList.add(Event(item))
            }
            mEventList.postValue(itemList)
        }
    }
}